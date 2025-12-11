// EQ.Core/Action/Composition/ActVision.cs
using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using Tcp;

//using static EQ.Domain.Entities;

namespace EQ.Core.Act
{
    public class ActVision : ActComponent
    {
        private readonly ConcurrentDictionary<string, ITcpNetworkClient> _clients = new();

       
        /// <summary>
        /// 모든 비전 응답을 JSON 문자열 그대로 전달하는 이벤트 (디버그 UI용)
        /// </summary>
        public event Action<string, string> OnRawDataReceived;

       
        public event Action<string, VisionCommandType, object> OnCommandReceived;
        // ------------------------------------------

        public ActVision(ACT act) : base(act) { }

        public void RegisterClient(string name, ITcpNetworkClient client)
        {
            if (_clients.TryAdd(name, client))
            {
                client.OnRead += (packet) => HandleVisionData(name, packet);
                client.OnConnected += () => Log.Instance.Info($"[ActVision] {name} Connected.");
                client.OnDisconnected += () => Log.Instance.Warning($"[ActVision] {name} Disconnected.");
            }
        }

      
        public ITcpNetworkClient GetClient(string name)
        {
            _clients.TryGetValue(name, out var client);
            return client; // 없으면 null 반환
        }
       
        public async Task SendGenericCommand(string clientName, object commandObject)
        {
            if (_clients.TryGetValue(clientName, out var client) && client.IsConnected)
            {
                try
                {
                    if(false) // 일반적인 json
                    {
                       

                        // 직렬화 (Null 값은 무시하여 "ReelID1": null 같은 항목 제거)
                        string json = JsonConvert.SerializeObject(commandObject,
                                        Formatting.None,
                                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                        Log.Instance.Debug($"[ActVision] SEND to {clientName}: {json}");
                        await client.SendData(json);
                    }
                    else
                    {
                        //원래 필요 없으나 VC120 용 json과 통신하기 위해서 사용... value0 이라는 root를 만들지 않으면 직렬화,역직렬화 안됨
                        var wrapper = new { value0 = commandObject };                      

                        // 직렬화 (Null 값은 무시하여 "ReelID1": null 같은 항목 제거)
                        string json = JsonConvert.SerializeObject(wrapper,
                                        Formatting.None,
                                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                        Log.Instance.Debug($"[ActVision] SEND to {clientName}: {json}");
                        await client.SendData(json);
                    }

                   
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[ActVision] SendGenericCommand 실패: {ex.Message}");
                }
            }
        }

        // (수정) OnRead 이벤트 핸들러 (JSON 역직렬화 및 이벤트 발행)
        private void HandleVisionData(string clientName, PacketData data)
        {
            try
            {
                Log.Instance.Debug($"[ActVision] RECV from {clientName}: {data.Str}");
                OnRawDataReceived?.Invoke(clientName, data.Str);

                ////////////////////////////  VC120과 통신 하기 위해 추가 - 일반 Json은 필요 없음
                if(true)
                {
                    string cleanJson = data.Str;

                    JObject rootObject = JObject.Parse(data.Str);

                    // value0 필드가 존재하면 root값이 불필요하게 생성 된 것이므로 제거
                    if (rootObject.TryGetValue("value0", out JToken value0Token))
                    {
                        cleanJson = value0Token.ToString(Formatting.None);
                    }
                    data.Str = cleanJson;
                }
                
                //////////////////////////////////////

                JObject json = JObject.Parse(data.Str);
                string cmd = (string)json["CMD"];

                if (string.IsNullOrEmpty(cmd)) return;

                if (Enum.TryParse<VisionCommandType>(cmd, out var cmdType))
                {
                    object parsedObject = null;
                    switch (cmdType)
                    {
                      
                        case VisionCommandType.EOT:
                            parsedObject = JsonConvert.DeserializeObject<EOT>(data.Str);
                            break;

                      
                        case VisionCommandType.GrabDone:
                            parsedObject = JsonConvert.DeserializeObject<GrabDone>(data.Str);
                            break;

                     
                        case VisionCommandType.LotEnd:
                            parsedObject = JsonConvert.DeserializeObject<LotEnd>(data.Str);
                            break;

                        case VisionCommandType.SOT:
                            parsedObject = JsonConvert.DeserializeObject<SOT>(data.Str);
                            break;
                            // ... 다른 수신 타입 ...
                    }

                    if (parsedObject != null)
                    {
                        // 이 이벤트가 정상적으로 호출됩니다.
                        OnCommandReceived?.Invoke(clientName, cmdType, parsedObject);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[ActVision] JSON 파싱 실패: {data.Str} | {ex.Message}");
            }
        }
    }
}