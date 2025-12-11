using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EQ.Core.Service
{
    // [데이터 모델 1] 요약 통계용
    public class LogStatResult
    {
        public string Type { get; set; } // SEQUENCE or ACTION
        public string Name { get; set; }
        public int Count { get; set; }
        public double AvgTime { get; set; }
        public long MinTime { get; set; }
        public long MaxTime { get; set; }
        public long TotalTime { get; set; }
    }

    public class StateTimelineItem
    {
        public DateTime Timestamp { get; set; }
        public string State { get; set; } // Idle, Running, Error ...
    }

    // [신규] 가동률 및 신뢰성 분석 결과 모델
    public class UtilizationResult
    {
        // 상태별 누적 시간 (Run, Idle, Error...)
        public Dictionary<string, TimeSpan> StateDurations { get; set; } = new Dictionary<string, TimeSpan>();

        public double Availability { get; set; } // 가동률 (%)
        public int FailureCount { get; set; }    // 고장 횟수
        public TimeSpan TotalRunTime { get; set; }
        public TimeSpan TotalDownTime { get; set; }

        public string MTBF { get; set; } // 문자열로 포맷팅 (예: "12h 30m")
        public string MTTR { get; set; }
    }


    // [데이터 모델 2] 상세 사이클 분석용
    public class SequenceCycleData
    {
        public DateTime Timestamp { get; set; }
        public string SequenceName { get; set; }
        public long TotalTime { get; set; }
        // 중복 스텝명을 허용하려면 List<KeyValuePair>가 낫지만, 보통 스텝명은 고유하므로 Dictionary 유지
        public Dictionary<string, long> Steps { get; set; } = new Dictionary<string, long>();
    }

    public class LogStatisticsService
    {
        public List<StateTimelineItem> GetStateTimeline(string filePath)
        {
            var timeline = new List<StateTimelineItem>();
            if (!File.Exists(filePath)) return timeline;

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // 로그 포맷 예시: [Time],[11:41:27:434],[SetState],[ActTowerLamp.cs(55)], [State] Idle
                    if (line.Contains("[State]"))
                    {
                        try
                        {
                            var parts = line.Split(',');
                            if (parts.Length < 2) continue;

                            // 시간 파싱 (parts[1]: [11:41:27:434])
                            string timeStr = parts[1].Trim().Trim('[', ']');

                            // 상태 파싱 (마지막 부분에서 [State] 뒤의 텍스트 추출)
                            string msgPart = parts.Last();
                            int idx = msgPart.IndexOf("[State]");
                            if (idx == -1) continue;

                            string state = msgPart.Substring(idx + 7).Trim(); // "[State]".Length = 7

                            if (TryParseLogTime(timeStr, out DateTime dt))
                            {
                                timeline.Add(new StateTimelineItem { Timestamp = dt, State = state });
                            }
                        }
                        catch { /* 파싱 에러 무시 */ }
                    }
                }
            }
            return timeline;
        }
        // 1. 통계 요약 분석 (Grid 표시용)
        public List<LogStatResult> AnalyzeSummary(string filePath)
        {
            var stats = new List<LogStatResult>();
            if (!File.Exists(filePath)) return stats;

            var entries = new List<(string Type, string Name, long Duration)>();

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // 포맷: [Time],[...],[...],[...], TYPE, Name, Duration
                    var parts = line.Split(',');
                    if (parts.Length >= 7)
                    {
                        string type = parts[4].Trim();
                        if ((type == "SEQUENCE" || type == "ACTION") && long.TryParse(parts[6].Trim(), out long duration))
                        {
                            entries.Add((type, parts[5].Trim(), duration));
                        }
                    }
                }
            }

            return entries
                .GroupBy(e => new { e.Type, e.Name })
                .Select(g => new LogStatResult
                {
                    Type = g.Key.Type,
                    Name = g.Key.Name,
                    Count = g.Count(),
                    AvgTime = Math.Round(g.Average(x => x.Duration), 1),
                    MinTime = g.Min(x => x.Duration),
                    MaxTime = g.Max(x => x.Duration),
                    TotalTime = g.Sum(x => x.Duration)
                })
                .OrderBy(s => s.Type).ThenByDescending(s => s.AvgTime)
                .ToList();
        }

        private bool TryParseLogTime(string timeStr, out DateTime result)
        {
            // 로그에 사용된 포맷 "HH:mm:ss:fff" (예: 12:18:15:999)
            return DateTime.TryParseExact(
                timeStr,
                "HH:mm:ss:fff",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result);
        }

        /// <summary>
        /// [신규] 가동률 및 MTBF/MTTR 분석
        /// </summary>
        public UtilizationResult AnalyzeUtilization(string filePath)
        {
            var result = new UtilizationResult();
            if (!File.Exists(filePath)) return result;

            // 상태별 시간 누적을 위한 변수
            var stateDurations = new Dictionary<string, TimeSpan>();
            foreach (string name in Enum.GetNames(typeof(EQ.Domain.Enums.EqState)))
                stateDurations[name] = TimeSpan.Zero;

            DateTime? lastTime = null;
            string lastState = "Init"; // 초기 상태 가정

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // 로그 포맷: [Info],[Time],..., [State] Running
                    if (line.Contains("[State]"))
                    {
                        try
                        {
                            var parts = line.Split(',');
                            string timeStr = parts[1].Trim('[', ']');

                            // 메시지 부분에서 상태 추출 (" [State] Running" -> "Running")
                            string msgPart = parts.Last();
                            int idx = msgPart.IndexOf("[State]");
                            if (idx == -1) continue;

                            string newState = msgPart.Substring(idx + 7).Trim(); // "[State]".Length = 7

                            if (TryParseLogTime(timeStr, out DateTime currentTime))
                            {
                                // 이전 상태의 지속 시간 계산
                                if (lastTime.HasValue)
                                {
                                    TimeSpan duration = currentTime - lastTime.Value;

                                    if (stateDurations.ContainsKey(lastState))
                                        stateDurations[lastState] += duration;
                                    else
                                        stateDurations[lastState] = duration;
                                }

                                // 에러 발생 횟수 카운트 (Error 상태로 진입한 경우)
                                if (newState == "Error" && lastState != "Error")
                                {
                                    result.FailureCount++;
                                }

                                lastTime = currentTime;
                                lastState = newState;
                            }
                        }
                        catch { }
                    }
                }

                // 마지막 상태부터 로그 끝(파일 닫는 시점)까지는 계산이 어려우므로 생략하거나, 
                // 현재 시간이 로그 날짜와 같다면 현재 시간까지로 계산할 수도 있음. 
                // 여기서는 단순화를 위해 마지막 로그 시점까지만 계산.
            }

            result.StateDurations = stateDurations;

            // 지표 계산
            result.TotalRunTime = stateDurations.ContainsKey("Running") ? stateDurations["Running"] : TimeSpan.Zero;
            result.TotalDownTime = stateDurations.ContainsKey("Error") ? stateDurations["Error"] : TimeSpan.Zero;

            // 가동률 = Run / (Run + Idle + Error + Init)  (혹은 전체 시간)
            double totalTotal = stateDurations.Values.Sum(t => t.TotalSeconds);
            if (totalTotal > 0)
                result.Availability = (result.TotalRunTime.TotalSeconds / totalTotal) * 100.0;

            // MTBF = Total Run Time / Failure Count
            if (result.FailureCount > 0)
            {
                double mtbfSec = result.TotalRunTime.TotalSeconds / result.FailureCount;
                double mttrSec = result.TotalDownTime.TotalSeconds / result.FailureCount;

                result.MTBF = TimeSpan.FromSeconds(mtbfSec).ToString(@"hh\:mm\:ss");
                result.MTTR = TimeSpan.FromSeconds(mttrSec).ToString(@"hh\:mm\:ss");
            }
            else
            {
                result.MTBF = "∞";
                result.MTTR = "00:00:00";
            }

            return result;
        }


        // 2. 상세 사이클 분석 (Chart 표시용)
        public List<SequenceCycleData> AnalyzeCycles(string filePath)
        {
            var cycles = new List<SequenceCycleData>();
            if (!File.Exists(filePath)) return cycles;

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                string line;
                SequenceCycleData currentCycle = null;

                // [수정] 정규식 개선: 괄호 사이의 스텝 이름과 숫자를 좀 더 유연하게 찾음
                // 예: "- Step [Start] : 1 ms"
                var stepRegex = new Regex(@"Step\s*\[(.+?)\]\s*:\s*(\d+)\s*ms", RegexOptions.Compiled);

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("Cycle Completed. Step Times:"))
                    {
                        // 이전 사이클 저장
                        if (currentCycle != null && currentCycle.Steps.Count > 0)
                        {
                            if (currentCycle.TotalTime == 0) currentCycle.TotalTime = currentCycle.Steps.Values.Sum();
                            cycles.Add(currentCycle);
                        }

                        // 새 사이클 시작
                        try
                        {
                            currentCycle = new SequenceCycleData { Steps = new Dictionary<string, long>() };

                            // 시간 파싱
                            var parts = line.Split(',');
                            string timeStr = parts[1].Trim('[', ']');
                            if (TryParseLogTime(timeStr, out DateTime dt)) currentCycle.Timestamp = dt;

                            // 시퀀스 이름 파싱
                            string msgPart = parts.Last();
                            int start = msgPart.IndexOf('[');
                            int end = msgPart.IndexOf(']');
                            if (start >= 0 && end > start)
                                currentCycle.SequenceName = msgPart.Substring(start + 1, end - start - 1);
                        }
                        catch { currentCycle = null; }
                    }
                    else if (currentCycle != null && line.Trim().StartsWith("- Step"))
                    {
                        var match = stepRegex.Match(line);
                        if (match.Success)
                        {
                            string stepName = match.Groups[1].Value.Trim(); // 스텝 이름
                            if (long.TryParse(match.Groups[2].Value, out long duration))
                            {
                                // [중요] 키 중복 방지 (혹시 같은 스텝명이 2번 찍힐 경우)
                                if (currentCycle.Steps.ContainsKey(stepName))
                                    stepName += $"_{currentCycle.Steps.Count}";

                                currentCycle.Steps.Add(stepName, duration);
                            }
                        }
                    }
                    // 사이클 종료 조건 (빈 줄 등)
                    else if (currentCycle != null && string.IsNullOrWhiteSpace(line))
                    {
                        if (currentCycle.Steps.Count > 0)
                        {
                            currentCycle.TotalTime = currentCycle.Steps.Values.Sum();
                            cycles.Add(currentCycle);
                        }
                        currentCycle = null;
                    }
                }
                // 마지막 데이터 처리
                if (currentCycle != null && currentCycle.Steps.Count > 0)
                {
                    currentCycle.TotalTime = currentCycle.Steps.Values.Sum();
                    cycles.Add(currentCycle);
                }
            }
            return cycles;
        }
    }
}