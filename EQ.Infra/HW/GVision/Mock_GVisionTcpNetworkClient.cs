using EQ.Common.Logs;
using EQ.Domain.Entities;
using EQ.Domain.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tcp // (EqBase.Infra 프로젝트 내의 'Tcp' 네임스페이스)
{
    /// <summary>
    /// ITcpNetworkClient의 Mock(가짜) 구현체입니다.
    /// VisionSkip 등 시뮬레이션 용도로 사용되며, 실제 네트워크 연결 없이 정해진 응답을 반환합니다.
    /// </summary>
    public class Mock_GVisionTcpNetworkClient : ITcpNetworkClient
    {
        // ITcpNetworkClient 인터페이스 이벤트
        public event Action<PacketData> OnRead;
        public event Action OnConnected;
        public event Action OnDisconnected;

        /// <summary>
        /// Mock 규칙 저장소
        /// Key: string (요청 CMD 이름, 예: "SOT", "JobChange")
        /// Value: Func<string> (응답 JSON 문자열을 생성하는 팩토리 함수)
        /// </summary>
        private readonly Dictionary<string, Func<string>> _rules = new();

        private string _name, _ip;
        private int _port;
        private bool _isConnected = false;

        public bool IsConnected => _isConnected;

        public Mock_GVisionTcpNetworkClient()
        {
            Log.Instance.Info("[MockTcpNetworkClient] MOCK 클라이언트 인스턴스 생성됨.");
        }

        /// <summary>
        /// (ITcpNetworkClient 구현)
        /// Mock 클라이언트를 초기화하고 즉시 '가짜' 연결 성공 이벤트를 발생시킵니다.
        /// </summary>
        public void Init(string name, string ip, int port, int bufSize = 8192, bool autoReconnect = true, EndType endType = EndType.None)
        {
            _name = name;
            _ip = ip;
            _port = port;

            Log.Instance.Warning($"[MockClient {_name}] ⚠️ MOCK 모드 초기화 (Fake Connect). Target: {ip}:{port}");

            if (_isConnected) return; // 이미 연결됨

            _isConnected = true;
            // 실제 연결처럼 비동기로 이벤트 발생
            Task.Run(() => OnConnected?.Invoke());
        }

        /// <summary>
        /// (ITcpNetworkClient 구현)
        /// '가짜' 연결을 종료하고 연결 종료 이벤트를 발생시킵니다.
        /// </summary>
        public void Close()
        {
            if (!_isConnected) return;

            _isConnected = false;
            Log.Instance.Warning($"[MockClient {_name}] MOCK 연결 종료 (Fake Disconnect).");
            Task.Run(() => OnDisconnected?.Invoke());
        }

        /// <summary>
        /// (Mock 전용) 정적(static) 응답 규칙을 추가합니다.
        /// (요청 CMD -> 고정된 응답 객체)
        /// </summary>
        public void AddRule<TReceive>(string command, TReceive responseData)
        {
            try
            {
                // 응답 객체를 미리 직렬화
                string receiveJson = JsonConvert.SerializeObject(responseData);
                // 항상 고정된 JSON을 반환하는 람다를 딕셔너리에 저장
                _rules[command] = () => receiveJson;
                Log.Instance.Debug($"[MockClient {_name}] 정적 규칙 추가: {command} -> {receiveJson}");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[MockClient {_name}] 정적 규칙 추가 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// (Mock 전용) 동적(dynamic) 응답 규칙을 Func 델리게이트로 추가합니다.
        /// (요청 CMD -> 람다 함수 실행 -> 동적 응답 객체)
        /// </summary>
        public void AddRule<TReceive>(string command, Func<TReceive> responseFactory)
        {
            try
            {
                // Func<TReceive>를 Func<string>으로 래핑하여 저장
                // (호출될 때마다 responseFactory()가 실행되고, 그 결과를 직렬화)
                _rules[command] = () => JsonConvert.SerializeObject(responseFactory());
                Log.Instance.Debug($"[MockClient {_name}] 동적 규칙 추가: {command} -> (Dynamic Response Factory)");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[MockClient {_name}] 동적 규칙 추가 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// (ITcpNetworkClient 구현)
        /// '가짜' 데이터 전송을 시뮬레이션합니다.
        /// 전송된 데이터(JSON)에서 'CMD'를 파싱하여 등록된 규칙을 찾고,
        /// 일치하는 규칙이 있으면 가짜 응답(OnRead)을 생성합니다.
        /// </summary>
        public Task SendData(string data)
        {
            if (!_isConnected) return Task.CompletedTask;

            string commandJson = data.Trim();
            Log.Instance.Info($"[MockClient {_name}] SEND: {commandJson}");

            try
            {
                // 1. 수신된 JSON을 파싱하여 CMD 값을 추출
                JObject json = JObject.Parse(commandJson);
                string cmd = (string)json["CMD"];

                if (string.IsNullOrEmpty(cmd))
                {
                    Log.Instance.Warning($"[MockClient {_name}] CMD 속성을 찾을 수 없습니다: {commandJson}");
                    return Task.CompletedTask;
                }

                // 2. CMD 문자열로 규칙을 검색
                if (_rules.TryGetValue(cmd, out Func<string> responseFactory))
                {
                    // 3. 팩토리 함수를 "실행"하여 "새로운" 응답 JSON을 생성
                    string responseJson = responseFactory();

                    Log.Instance.Info($"[MockClient {_name}] RECV (규칙 일치: {cmd}): {responseJson}");

                    byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);
                    var packet = new PacketData
                    {
                        Name = _name,
                        Ip = _ip,
                        Port = _port.ToString(),
                        Str = responseJson,
                        Bytes = responseBytes
                    };

                    // 4. 가짜 네트워크 지연(50ms) 후 OnRead 이벤트 발생
                    Task.Run(async () =>
                    {
                        await Task.Delay(50);
                        OnRead?.Invoke(packet);
                    });
                }
                else
                {
                    Log.Instance.Warning($"[MockClient {_name}] RECV (규칙 없음): {cmd}에 대한 응답 규칙이 없습니다.");
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[MockClient {_name}] JSON 파싱 또는 규칙 실행 실패: {ex.Message} | DATA: {commandJson}");
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// (ITcpNetworkClient 구현)
        /// 바이트 배열도 UTF8 문자열로 변환하여 동일하게 처리합니다.
        /// </summary>
        public Task SendData(byte[] data)
        {
            return SendData(Encoding.UTF8.GetString(data));
        }
    }
}