using EQ.Common.Helper;
using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.Infra.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EQ.Core.Act.Composition
{

    /// <summary>
    /// 차트 데이터 수집 및 관리
    /// 외부에서 AddData()를 호출하여 데이터를 주입하는 방식
    /// </summary>
    public class ActChartData : ActComponent
    {
        private readonly ACT _act;
        private readonly Dictionary<string, List<DataPoint>> _dataBuffers;
        private readonly object _lock = new object();
        private readonly ChartDataStorage _storage;

        public ActChartData(ACT act) : base(act)
        {
            _act = act;
            _dataBuffers = new Dictionary<string, List<DataPoint>>();

            // DB Storage 초기화
            string dbPath = Path.Combine(@"d:\ChartDB", "chart_data.db");
            _storage = new ChartDataStorage(dbPath);

            // 각 항목별 List 초기화 (초기 용량 15000 - 약 4시간 분량)
            foreach (var item in Enum.GetValues<ChartTypes>())
            {
                _dataBuffers[item.ToString()] = new List<DataPoint>(15000);
            }

            Log.Instance.Info("ActChartData initialized");
        }

        bool threadStarted = false;
        public void start()
        {
            Log.Instance.Info("ActChartData started");
            Clear();
            threadStarted = true;
            var _act = ActManager.Instance.Act;

            Task.Run(async() =>
            {
               while (threadStarted)
               {
                    DateTime dateTime = DateTime.Now;
                    //모터
                    {
                        var sp = _act.Motion.GetStatus(MotionID.FEEDER_T);
                        AddData(ChartTypes.FEEDER_RPM, dateTime, sp.ActualVelocity);
                        var trq  =CalcTorque.ToNcm(SGMXJModel._04A, sp.ActualTorque).ToString("F1");
                        AddData(ChartTypes.FEEDER_TRQ, dateTime, double.Parse(trq));
                        AddData(ChartTypes.FEEDER_SPEED, dateTime, sp.ActualVelocity); // 기어비 맞게 변환 필요

                        sp = _act.Motion.GetStatus(MotionID.SCREW_T);
                        AddData(ChartTypes.EXTRUDER_RPM, dateTime, sp.ActualVelocity);
                        trq = CalcTorque.ToNcm(SGMXJModel._04A, sp.ActualTorque).ToString("F1");
                        AddData(ChartTypes.EXTRUDER_TRQ, dateTime, double.Parse(trq));
                        AddData(ChartTypes.EXTRUDER_SPEED, dateTime, sp.ActualVelocity); // 기어비 맞게 변환 필요

                        sp = _act.Motion.GetStatus(MotionID.PULLER_T);
                        AddData(ChartTypes.PULLER_RPM, dateTime, sp.ActualVelocity);
                        trq = CalcTorque.ToNcm(SGMXJModel._04A, sp.ActualTorque).ToString("F1");
                        AddData(ChartTypes.PULLER_TRQ, dateTime, double.Parse(trq));
                        AddData(ChartTypes.PULLER_SPEED, dateTime, sp.ActualVelocity); // 기어비 맞게 변환 필요

                    }

                    //온도
                    {
                        var all = _act.Temp.GetAll();
                        all[0].ReadSV();

                        AddData(ChartTypes.ZONE1_TEMP_PV, dateTime, all[0].ReadPV());
                        AddData(ChartTypes.ZONE1_TEMP_SV, dateTime, all[0].ReadSV());
                        AddData(ChartTypes.ZONE2_TEMP_PV, dateTime, all[1].ReadPV());
                        AddData(ChartTypes.ZONE2_TEMP_SV, dateTime, all[1].ReadSV());
                        AddData(ChartTypes.ZONE3_TEMP_PV, dateTime, all[2].ReadPV());
                        AddData(ChartTypes.ZONE3_TEMP_SV, dateTime, all[2].ReadSV());
                        AddData(ChartTypes.ZONE4_TEMP_PV, dateTime, all[3].ReadPV());
                        AddData(ChartTypes.ZONE4_TEMP_SV, dateTime, all[3].ReadSV());                                               
                    }

                    //품질
                    {
                        var quality = 0f;
                        AddData(ChartTypes.DIAMETER, dateTime, quality);
                        AddData(ChartTypes.OVALITY, dateTime, quality);
                        AddData(ChartTypes.GOOD_COUNT, dateTime, quality);
                        AddData(ChartTypes.BAD_COUNT, dateTime, quality);
                    }
                    await Task.Delay(500);
                }
            });
        }

        /// <summary>
        /// 데이터 포인트 추가 (현재 시간)
        /// </summary>
        public void AddData(ChartTypes item, double value)
        {
            lock (_lock)
            {
                if (!_dataBuffers.ContainsKey(item.ToString()))
                {
                    Log.Instance.Warning($"Unknown chart data item: {item}");
                    return;
                }

                _dataBuffers[item.ToString()].Add(new DataPoint
                {
                    Timestamp = DateTime.Now,
                    Value = value
                });
            }
        }

        /// <summary>
        /// 데이터 포인트 추가 (타임스탬프 지정)
        /// </summary>
        public void AddData(ChartTypes item, DateTime timestamp, double value)
        {
            lock (_lock)
            {
                if (!_dataBuffers.ContainsKey(item.ToString()))
                {
                    Log.Instance.Warning($"Unknown chart data item: {item}");
                    return;
                }

                _dataBuffers[item.ToString()].Add(new DataPoint
                {
                    Timestamp = timestamp,
                    Value = value
                });
            }
        }

        /// <summary>
        /// 특정 항목의 데이터 조회
        /// </summary>
        public List<DataPoint> GetData(ChartTypes item, DateTime? start = null, DateTime? end = null)
        {
            lock (_lock)
            {
                if (!_dataBuffers.ContainsKey(item.ToString()))
                    return new List<DataPoint>();

                var data = _dataBuffers[item.ToString()];

                // 시간 범위 필터링
                if (start.HasValue && end.HasValue)
                {
                    return data.Where(p => p.Timestamp >= start && p.Timestamp <= end).ToList();
                }

                return data.ToList(); // 전체 반환
            }
        }

        /// <summary>
        /// 최신 값 조회
        /// </summary>
        public double? GetLatestValue(ChartTypes item)
        {
            lock (_lock)
            {
                if (!_dataBuffers.ContainsKey(item.ToString()))
                    return null;

                var data = _dataBuffers[item.ToString()];
                return data.Count > 0 ? data[data.Count - 1].Value : null;
            }
        }

        /// <summary>
        /// 특정 인덱스 이후의 데이터만 조회 (증분 업데이트용)
        /// </summary>
        /// <param name="item">데이터 항목</param>
        /// <param name="fromIndex">시작 인덱스 (이 인덱스 이후의 데이터만 반환)</param>
        /// <returns>새로운 데이터 리스트</returns>
        public List<DataPoint> GetDataSince(ChartTypes item, int fromIndex)
        {
            lock (_lock)
            {
                if (!_dataBuffers.ContainsKey(item.ToString()))
                    return new List<DataPoint>();

                var data = _dataBuffers[item.ToString()];
                
                // fromIndex 이후의 데이터만 반환
                if (fromIndex < 0 || fromIndex >= data.Count)
                    return new List<DataPoint>();

                return data.Skip(fromIndex).ToList();
            }
        }

        /// <summary>
        /// 특정 시간 이후의 데이터만 조회 (증분 업데이트용)
        /// </summary>
        /// <param name="item">데이터 항목</param>
        /// <param name="afterTime">이 시간 이후의 데이터만 반환</param>
        /// <returns>새로운 데이터 리스트</returns>
        public List<DataPoint> GetDataAfter(ChartTypes item, DateTime afterTime)
        {
            lock (_lock)
            {
                if (!_dataBuffers.ContainsKey(item.ToString()))
                    return new List<DataPoint>();

                var data = _dataBuffers[item.ToString()];
                return data.Where(p => p.Timestamp > afterTime).ToList();
            }
        }

        /// <summary>
        /// 모든 데이터 초기화
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                foreach (var list in _dataBuffers.Values)
                {
                    list.Clear();
                }
            }
            Log.Instance.Info("Chart data cleared");
        }

        /// <summary>
        /// CSV 파일로 내보내기
        /// </summary>
        public bool ExportToCSV(string filePath)
        {
            try
            {
                lock (_lock)
                {
                    // 디렉토리 생성
                    var directory = Path.GetDirectoryName(filePath);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                    {
                        // 헤더
                        writer.WriteLine("Timestamp," + string.Join(",", Enum.GetValues<ChartTypes>()));

                        // 모든 항목의 최대 개수 찾기
                        int maxCount = _dataBuffers.Values.Max(list => list.Count);

                        // 라인별로 출력
                        for (int i = 0; i < maxCount; i++)
                        {
                            var line = new List<string>();

                            // 타임스탬프 (첫 번째 항목 기준)
                            var firstList = _dataBuffers.Values.FirstOrDefault();
                            if (firstList != null && i < firstList.Count)
                            {
                                line.Add(firstList[i].Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                            else
                            {
                                line.Add("");
                            }

                            // 각 항목의 값
                            foreach (var item in Enum.GetValues<ChartTypes>())
                            {
                                if (_dataBuffers[item.ToString()].Count > i)
                                {
                                    line.Add(_dataBuffers[item.ToString()][i].Value.ToString("F2"));
                                }
                                else
                                {
                                    line.Add("");
                                }
                            }

                            writer.WriteLine(string.Join(",", line));
                        }
                    }

                    Log.Instance.Info($"Chart data exported to CSV: {filePath}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Failed to export CSV: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 현재 데이터를 DB에 저장
        /// </summary>
        public bool SaveToDatabase(string runName)
        {
            lock (_lock)
            {
                Log.Instance.Info($"Saving chart data to database: {runName}");
                bool success = _storage.SaveRun(runName, _dataBuffers);

                if (success)
                {
                    int totalCount = _dataBuffers.Values.Sum(list => list.Count);
                    Log.Instance.Info($"Chart data saved successfully: {runName} ({totalCount} points)");
                }
                else
                {
                    Log.Instance.Error($"Failed to save chart data: {runName}");
                }

                return success;
            }
        }

        /// <summary>
        /// DB에서 데이터 로딩
        /// </summary>
        public bool LoadFromDatabase(string runName)
        {
            lock (_lock)
            {
                Log.Instance.Info($"Loading chart data from database: {runName}");

                var loadedData = _storage.LoadRun(runName);

                if (loadedData != null)
                {
                    // 기존 데이터는 초기화하지 않고 로드된 데이터로 대체
                    foreach (var item in Enum.GetValues<ChartTypes>())
                    {
                        if (loadedData.ContainsKey(item.ToString()))
                        {
                            _dataBuffers[item.ToString()] = loadedData[item.ToString()];
                        }
                        else
                        {
                            _dataBuffers[item.ToString()] = new List<DataPoint>(15000);
                        }
                    }

                    int totalCount = _dataBuffers.Values.Sum(list => list.Count);
                    Log.Instance.Info($"Chart data loaded successfully: {runName} ({totalCount} points)");
                    return true;
                }
                else
                {
                    Log.Instance.Error($"Failed to load chart data: {runName}");
                    return false;
                }
            }
        }

        /// <summary>
        /// 저장된 Run 목록 조회
        /// </summary>
        public List<string> GetSavedRunNames()
        {
            return _storage.GetRunNames();
        }

        /// <summary>
        /// 현재 데이터 개수 조회
        /// </summary>
        public int GetDataCount(ChartTypes item)
        {
            lock (_lock)
            {
                if (!_dataBuffers.ContainsKey(item.ToString()))
                    return 0;

                return _dataBuffers[item.ToString()].Count;
            }
        }
    }
}
