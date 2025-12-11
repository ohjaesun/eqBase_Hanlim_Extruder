using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Enums;

using EQ.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EQ.Core.Act
{
    public class ActPIO : ActComponent
    {
        private readonly Dictionary<PIOId, IPIOHandover> _clients = new Dictionary<PIOId, IPIOHandover>();
        private readonly Dictionary<PIOId, PIOState> _clientStates = new Dictionary<PIOId, PIOState>();
        private string Name => "PIO";

        public ActPIO(ACT act) : base(act)
        {
            // 모든 PIO 포트의 초기 상태를 Idle로 설정
            foreach (PIOId id in Enum.GetValues(typeof(PIOId)))
            {
                _clientStates[id] = PIOState.Idle;
            }
        }

        /// <summary>
        /// FormSplash에서 생성된 하드웨어 컨트롤러 클라이언트를 PIO ID별로 등록합니다.
        /// </summary>
        public void RegisterClient(PIOId id, IPIOHandover client , IO_IN startInput , IO_OUT startOut)
        {
            if (_clients.TryAdd(id, client))
            {
                client.SetIOStartIndex(startInput, startOut);
                Log.Instance.Info(Name, $"PIO Client '{id}' registered.");
            }
        }
        
        public PIOState GetCurrentState(PIOId id)
        {
            _clientStates.TryGetValue(id, out var state);
            return state;
        }

        public bool GetSignal(PIOId id, PIOSignal signal)
        {
            if (_clients.TryGetValue(id, out var client))
            {
                return client.GetSignal(signal);
            }
            Log.Instance.Warning(Name, $"PIO Client '{id}' not found. Cannot get signal.");
            return false;
        }

        #region [Load - 자재 반입 (수신)]

        /// <summary>
        /// 1단계: 반입 요청(L_REQ)을 켜고 상대 장비의 준비(READY)를 기다립니다.
        /// </summary>
        public async Task<ActionStatus> LoadReqAsync(PIOId id)
        {
            if (!_clients.TryGetValue(id, out var client))
            {            
                _act.PopupAlarm(ErrorList.PIO_INIT_FAIL, L("{0} is null", id));
                return ActionStatus.Error;
            }

            var stepNames = new List<string> { "CheckState", "Set_L_REQ", "Wait_READY" };

            return await _act.ExecuteAction(
                title: Globals.L("PIO.LoadReq.{0}", id),
                stepNames: stepNames,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;
                    
                        

                    switch (stepName)
                    {
                        case "CheckState":
                            if (_clientStates[id] != PIOState.Idle)
                            {
                                Log.Instance.Error(Name, Globals.L("PIO '{0}' is busy.", id));
                                context.Status = ActionStatus.Error;
                                _act.PopupAlarm(ErrorList.PIO_INIT_FAIL, L("{0} status is not IDLE", id));
                                break;
                            }
                            _clientStates[id] = PIOState.RequestingLoad;
                            nextStep++;
                            break;

                        case "Set_L_REQ":
                            client.SetSignal(PIOSignal.L_REQ, true);
                            Log.Instance.Info(Name, Globals.L("[{0}] L_REQ ON", id));
                            nextStep++;
                            break;

                        case "Wait_READY":                          

                            if (client.GetSignal(PIOSignal.READY))
                            {
                                Log.Instance.Info(Name, Globals.L("[{0}] READY Received", id));
                                // 여기서 Finished를 반환하면 시퀀스로 제어권이 넘어갑니다.
                                context.Status = ActionStatus.Finished;
                            }
                            else
                            {
                                await Task.Delay(50);
                                return context.StepIndex;
                            }
                            break;
                    }
                    return nextStep;
                }
            );
        }

        /// <summary>
        /// 2단계: 자재 이동 완료 후 신호를 끄고 핸드셰이크를 종료합니다.
        /// </summary>
        public async Task<ActionStatus> LoadCompAsync(PIOId id)
        {
            if (!_clients.TryGetValue(id, out var client))
            {
                _act.PopupAlarm(ErrorList.PIO_INIT_FAIL, L("{0} is null", id));
                return ActionStatus.Error;
            }

            var stepNames = new List<string> { "Reset_L_REQ", "Wait_READY_OFF", "Check_Complete" };

            return await _act.ExecuteAction(
                title: Globals.L("PIO.LoadComp.{0}", id),
                stepNames: stepNames,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;
                   

                    switch (stepName)
                    {
                        case "Reset_L_REQ":
                            client.SetSignal(PIOSignal.L_REQ, false);
                            Log.Instance.Info(Name, Globals.L("[{0}] L_REQ OFF", id));
                            nextStep++;
                            break;

                        case "Wait_READY_OFF":
                            if (!client.GetSignal(PIOSignal.READY))
                            {
                                Log.Instance.Info(Name, Globals.L("[{0}] READY OFF Checked", id));
                                nextStep++;
                            }
                            else
                            {                              
                                await Task.Delay(50);                            
                            }
                            break;

                        case "Check_Complete":
                            _clientStates[id] = PIOState.Idle;
                            context.Status = ActionStatus.Finished;
                            break;
                    }
                    return nextStep;
                }
            );
        }

        #endregion

        #region [Unload - 자재 반출 (송신)]

        /// <summary>
        /// 1단계: 반출 요청(U_REQ)을 켜고 상대 장비의 준비(READY)를 기다립니다.
        /// </summary>
        public async Task<ActionStatus> UnloadReqAsync(PIOId id)
        {
            if (!_clients.TryGetValue(id, out var client))
            {
                _act.PopupAlarm(ErrorList.PIO_INIT_FAIL, L("{0} is null", id));
                return ActionStatus.Error;
            }

            var stepNames = new List<string> { "CheckState", "Set_U_REQ", "Wait_READY" };

            return await _act.ExecuteAction(
                title: Globals.L("PIO.UnloadReq.{0}", id),
                stepNames: stepNames,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;
                  

                    switch (stepName)
                    {
                        case "CheckState":
                            // Unload는 내가 줄 자재가 있는지 시퀀스 레벨에서 먼저 확인했겠지만, 
                            // 여기서는 PIO 상태만 확인합니다.
                            if (_clientStates[id] != PIOState.Idle)
                            {
                                context.Status = ActionStatus.Error;
                                _act.PopupAlarm(ErrorList.PIO_INIT_FAIL, L("{0} status is not IDLE", id));
                                break;
                            }
                            _clientStates[id] = PIOState.RequestingUnload;
                            nextStep++;
                            break;

                        case "Set_U_REQ":
                            client.SetSignal(PIOSignal.U_REQ, true);
                            Log.Instance.Info(Name, Globals.L("[{0}] U_REQ ON", id));
                            nextStep++;
                            break;

                        case "Wait_READY":                          

                            if (client.GetSignal(PIOSignal.READY))
                            {
                                Log.Instance.Info(Name, Globals.L("[{0}] READY Received", id));
                                context.Status = ActionStatus.Finished;
                            }
                            else
                            {
                                await Task.Delay(50);                              
                            }
                            break;
                    }
                    return nextStep;
                }
            );
        }

        /// <summary>
        /// 2단계: 자재 이동 완료 후 신호를 끄고 핸드셰이크를 종료합니다.
        /// </summary>
        public async Task<ActionStatus> UnloadCompAsync(PIOId id)
        {
            if (!_clients.TryGetValue(id, out var client))
            {
                _act.PopupAlarm(ErrorList.PIO_INIT_FAIL, L("{0} is null", id));
                return ActionStatus.Error;
            }

            var stepNames = new List<string> { "Reset_U_REQ", "Wait_READY_OFF", "Complete" };

            return await _act.ExecuteAction(
                title: Globals.L("PIO.UnloadComp.{0}", id),
                stepNames: stepNames,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;
                  

                    switch (stepName)
                    {
                        case "Reset_U_REQ":
                            client.SetSignal(PIOSignal.U_REQ, false);
                            Log.Instance.Info(Name, Globals.L("[{0}] U_REQ OFF", id));
                            nextStep++;
                            break;

                        case "Wait_READY_OFF":
                            if (!client.GetSignal(PIOSignal.READY))
                            {
                                nextStep++;
                            }
                            else
                            {                               
                                await Task.Delay(50);                            
                            }
                            break;

                        case "Complete":
                            _clientStates[id] = PIOState.Idle;
                            context.Status = ActionStatus.Finished;
                            break;
                    }
                    return nextStep;
                }
            );
        }

        #endregion

        private void ResetSignals(PIOId id, IPIOHandover client)
        {
            if (client == null) return;
            client.SetSignal(PIOSignal.L_REQ, false);
            client.SetSignal(PIOSignal.U_REQ, false);
         //   client.SetSignal(PIOSignal.MGZN_PRES, false);
            client.SetSignal(PIOSignal.TR_REQ, false);
            _clientStates[id] = PIOState.Idle;
            Log.Instance.Info(Name, $"PIO signals for '{id}' have been reset.");
        }
    }
}