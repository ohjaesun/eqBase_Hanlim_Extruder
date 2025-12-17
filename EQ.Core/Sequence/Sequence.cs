using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcp;
using static EQ.Core.Globals;

namespace EQ.Core.Sequence
{
    public partial class SEQ
    {
        public enum SeqName
        {
            Seq1_Extuder, //Seq1.cs .. to Seq[N]
            Seq2_시나리오명,
            Seq3_시나리오명,
            Seq4_시나리오명,
            Seq5_시나리오명,
            Seq6_시나리오명,
            Seq7_시나리오명,
            Seq8_시나리오명,
            Seq9_시나리오명,
            Seq10_시나리오명,
            Seq11_시나리오명,
            Seq12_시나리오명,
            Seq13_시나리오명,
            Seq14_시나리오명,
            Seq15_시나리오명,
        }

        private readonly ACT _act;
        private ConcurrentDictionary<SeqName, ISeqInterface> dicSeq = new ConcurrentDictionary<SeqName, ISeqInterface>();
       
        public SEQ(ACT act)
        {
            _act = act;
        }

        /// <summary>
        /// 시퀀스 추가 부분
        /// </summary>
        private ISeqInterface s1;
        private ISeqInterface s2;
        private ISeqInterface s3;
        private ISeqInterface s4;
        private ISeqInterface s5;
        private ISeqInterface s6;
        private ISeqInterface s7;
        private ISeqInterface s8;
        private ISeqInterface s9;
        private ISeqInterface s10;
        private ISeqInterface s11;
        private ISeqInterface s12;
        private ISeqInterface s13;
        private ISeqInterface s14;
        private ISeqInterface s15;


        public void InitSequence()
        {           
            s1 = new Seq01(this, _act);
            s2 = new Seq02(this, _act);
            s3 = new Seq03(this, _act);
            s4 = new Seq04(this, _act);
            s5 = new Seq05(this, _act);
            s6 = new Seq06(this, _act);
            s7 = new Seq07(this, _act);
            s8 = new Seq08(this, _act);
            s9 = new Seq09(this, _act);
            s10 = new Seq10(this, _act);
            s11 = new Seq11(this, _act);
            s12 = new Seq12(this, _act);
            s13 = new Seq13(this, _act);
            s14 = new Seq14(this, _act);
            s15 = new Seq15(this, _act);

            // 딕셔너리에 연결
            dicSeq.TryAdd(SeqName.Seq1_Extuder, s1);
            dicSeq.TryAdd(SeqName.Seq2_시나리오명, s2);
            dicSeq.TryAdd(SeqName.Seq3_시나리오명, s3);
            dicSeq.TryAdd(SeqName.Seq4_시나리오명, s4);
            dicSeq.TryAdd(SeqName.Seq5_시나리오명, s5);
            dicSeq.TryAdd(SeqName.Seq6_시나리오명, s6);
            dicSeq.TryAdd(SeqName.Seq7_시나리오명, s7);
            dicSeq.TryAdd(SeqName.Seq8_시나리오명, s8);
            dicSeq.TryAdd(SeqName.Seq9_시나리오명, s9);
            dicSeq.TryAdd(SeqName.Seq10_시나리오명, s10);
            dicSeq.TryAdd(SeqName.Seq11_시나리오명, s11);    
            dicSeq.TryAdd(SeqName.Seq12_시나리오명, s12);
            dicSeq.TryAdd(SeqName.Seq13_시나리오명, s13);
            dicSeq.TryAdd(SeqName.Seq14_시나리오명, s14);
            dicSeq.TryAdd(SeqName.Seq15_시나리오명, s15);
        }

        public ISeqInterface GetSequence(SeqName name)
        {
            if (dicSeq.TryGetValue(name, out ISeqInterface seq))
            {
                return seq;
            }
            
            Log.Instance.Error($"등록되지 않은 시퀀스 요청: {name}");
            
            return null; // 못 찾으면 null 반환
        }

        public void RunSequence(SeqName seqName)
        {

            var p = dicSeq[seqName] ;

            if (p._Status == SeqStatus.STOP)
            {            
                p._Status = SeqStatus.RUN;
                p._Step = 0;
               
                foreach (var pp in p._StepTime)
                    pp.Value.Reset();

                // 3. [감시] Watchdog: 타임아웃 감시용 토큰 및 태스크
                long limitTime = _act.Option.Option4.MaxSequenceTime;
                if (limitTime <= 0) limitTime = 60000;
              
                var ctsWatchdog = new CancellationTokenSource();

                // 별도 스레드에서 주기적으로 타임 아웃 검사
                _ = Task.Run(async () =>
                {
                    while (!ctsWatchdog.IsCancellationRequested)
                    {
                        
                        if (p._Status != SeqStatus.RUN && p._Status != SeqStatus.SEQ_STOPPING)
                            break;

                        // [조건] 개별 스텝 시간의 합계 계산
                        long totalElapsedTime = p._StepTime.Values.Sum(sw => sw.ElapsedMilliseconds);

                        if (totalElapsedTime > limitTime)
                        {                           
                            p._Status = SeqStatus.TIMEOUT;
                            Log.Instance.Debug($"[{seqName}] Timeout Detected! (Total: {totalElapsedTime}ms > Limit: {limitTime}ms)");
                            break;
                        }

                        await Task.Delay(1000); 
                    }
                }, ctsWatchdog.Token);


                Task x = Task.Run(async () =>
                {
                    SequenceContext.CurrentSequenceId.Value = seqName.ToString();

                    try
                    {
                        var old_step = -1;

                        while (p._Status == SeqStatus.RUN)
                        {
                            //One Cycle Time
                            if (p._Step == 0)
                            {                              

                            }

                            if (p._StepMax <= p._Step)
                            {
                                p._Status = SeqStatus.STOP;
                                break;
                            }

                            if (old_step != p._Step)
                            {
                                old_step = p._Step;
                                Log.Instance.Sequence($"Seq,{seqName},Step:[{p._StepString}]");

                                p._StepTime[p._StepString].Restart();
                            }

                            await p.doSequence();
                            //p.doSequence().Wait();

                            //One Cycle Time
                            if (p._StepMax - 1 == old_step)
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.AppendLine($"[{seqName}] Cycle Completed. Step Times:");

                                var sortedSteps = p.GetSortedStepTimes();
                                long sum = 0;
                                foreach (var item in sortedSteps)
                                {                                  
                                    sb.AppendLine($" - Step [{item.StepName}] : {item.Time} ms");
                                    sum += item.Time;
                                }

                                Log.Instance.Time(sb.ToString());
                                Log.Instance.Time($"SEQUENCE,{seqName},{sum}");
                            }

                        }

                        if (p._Status == SeqStatus.STOP) // 시퀀스 끝등 정상 종료
                        {
                            p._StepTimeAllStop();
                            Log.Instance.Sequence($"Seq,{seqName},Status:[{p._Status}]");
                            p._Status = SeqStatus.STOP;                         
                        }
                        else if (p._Status == SeqStatus.SEQ_STOPPING) 
                        {
                            throw new OperationCanceledException("Stopping");                           
                        }
                        else if (p._Status == SeqStatus.TIMEOUT) 
                        {
                            throw new OperationCanceledException("timeOut");                         
                        }
                        else // 에러 종료
                        {
                            throw new OperationCanceledException("Error");                           
                        }
                    }                 
                    catch (Exception ex)
                    {

                        try
                        {                           
                            Log.Instance.Sequence($"Seq,{seqName},Stop Requested : {ex.Message}");
                            p._StepTimeAllStop();

                            if (p._Status == SeqStatus.SEQ_STOPPING)
                            {
                                Log.Instance.Sequence($"{seqName} OnStopping start");
                                await p.OnStopping();
                                Log.Instance.Sequence($"{seqName} OnStopping end");

                                p._Status = SeqStatus.STOP;
                            }
                            else if (p._Status == SeqStatus.ERROR || p._Status == SeqStatus.TIMEOUT)
                            {
                                if(p._Status == SeqStatus.ERROR)
                                    _act.PopupAlarm(ErrorList.SEQ_ERROR, L("{0},{1}[{2}]", seqName, p._StepString, p._Step));
                                if (p._Status == SeqStatus.TIMEOUT)
                                    _act.PopupAlarm(ErrorList.SEQ_TIMEOUT, L("{0},{1}[{2}]", seqName, p._StepString, p._Step));

                                Log.Instance.Sequence($"{seqName} OnErrorRecovery start");
                                await p.OnErrorRecovery();
                                Log.Instance.Sequence($"{seqName} OnErrorRecovery end");
                            }
                        }
                        catch (Exception innerEx)
                        {
                            _act.PopupAlarm(ErrorList.SEQ_ERROR, L("{0},예외 처리 중 에러 발생", seqName));

                            // [안전 장치] 내부에서 발생한 2차 예외를 잡아서 로그를 남김
                            Log.Instance.Error($"Seq,{seqName},CRITICAL ERROR during Stop/Recovery: {innerEx.Message}");

                            // 상태를 확실하게 STOP이나 ERROR로 강제 할당하여 시퀀스가 '유령 상태'가 되는 것을 방지
                            p._Status = SeqStatus.ERROR;
                        }                       
                    }                   
                    finally
                    {
                        SequenceContext.CurrentSequenceId.Value = null;
                    }                               
                });
            }
            else
            {
                Log.Instance.Info($"Seq,{seqName},can not Run -> Status,{p._Status}");
            }
        }

        public void ResetAllSequences()
        {
            foreach (var seq in dicSeq.Values)
            {
                // 에러나 타임아웃 상태인 시퀀스만 STOP으로 변경
                if (seq._Status == SeqStatus.ERROR || seq._Status == SeqStatus.TIMEOUT)
                {
                    seq._Status = SeqStatus.STOP;
                    seq._Step = 0; 

                    // 진행 시간 초기화
                    seq._StepTimeClear();
                }
            }
            Log.Instance.Info("[SEQ] All Sequences Reset to STOP.");
        }

        public void StoppingAllSequences()
        {
            foreach (var seq in dicSeq.Values)
            {             
                if (seq._Status == SeqStatus.RUN)
                {
                    seq._Status = SeqStatus.SEQ_STOPPING;                  
                }
            }
            Log.Instance.Info("[SEQ] All Sequences Reset to STOPPING.");
        }      

    }
}
