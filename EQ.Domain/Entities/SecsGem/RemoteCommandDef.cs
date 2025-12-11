using EQ.Domain.Enums.SecsGem;

namespace EQ.Domain.Entities.SecsGem
{
    /// <summary>
    /// Remote Command 정의
    /// SEMI E30 표준의 원격 명령을 정의합니다.
    /// S2F41/S2F42로 호스트에서 장비로 전송되는 명령입니다.
    /// </summary>
    public class RemoteCommandDef
    {
        /// <summary>
        /// 명령 이름 (RCMD)
        /// </summary>
        public string CommandName { get; set; } = string.Empty;

        /// <summary>
        /// 명령 설명
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 필수 파라미터 목록
        /// </summary>
        public List<CommandParameter> Parameters { get; set; } = new List<CommandParameter>();

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public RemoteCommandDef()
        {
        }

        /// <summary>
        /// 파라미터 생성자
        /// </summary>
        /// <param name="commandName">명령 이름</param>
        /// <param name="description">명령 설명</param>
        public RemoteCommandDef(string commandName, string description = "")
        {
            CommandName = commandName;
            Description = description;
        }

        /// <summary>
        /// 파라미터 추가
        /// </summary>
        /// <param name="name">파라미터 이름</param>
        /// <param name="isRequired">필수 여부</param>
        public void AddParameter(string name, bool isRequired = true)
        {
            Parameters.Add(new CommandParameter(name, isRequired));
        }

        /// <summary>
        /// 문자열 표현
        /// </summary>
        public override string ToString()
        {
            return string.Format("RCMD: {0} (Params: {1})", CommandName, Parameters.Count);
        }
    }

    /// <summary>
    /// Remote Command 파라미터
    /// </summary>
    public class CommandParameter
    {
        /// <summary>
        /// 파라미터 이름 (CPNAME)
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 필수 파라미터 여부
        /// </summary>
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// 기본값
        /// </summary>
        public string DefaultValue { get; set; } = string.Empty;

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public CommandParameter()
        {
        }

        /// <summary>
        /// 파라미터 생성자
        /// </summary>
        /// <param name="name">파라미터 이름</param>
        /// <param name="isRequired">필수 여부</param>
        public CommandParameter(string name, bool isRequired = true)
        {
            Name = name;
            IsRequired = isRequired;
        }
    }

    /// <summary>
    /// Remote Command 수신 데이터
    /// S2F41 메시지에서 파싱된 명령 데이터
    /// </summary>
    public class RemoteCommandData
    {
        /// <summary>
        /// 메시지 ID (lMsgId)
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// 명령 이름 (RCMD)
        /// </summary>
        public string CommandName { get; set; } = string.Empty;

        /// <summary>
        /// 파라미터 목록 (CPNAME, CPVAL)
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 응답 코드
        /// </summary>
        public HcAck ResponseCode { get; set; } = HcAck.Acknowledge;

        /// <summary>
        /// 파라미터 값 가져오기
        /// </summary>
        /// <param name="cpName">파라미터 이름</param>
        /// <returns>파라미터 값 (없으면 빈 문자열)</returns>
        public string GetParameterValue(string cpName)
        {
            if (Parameters.TryGetValue(cpName, out string? value) && value != null)
            {
                return value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 문자열 표현
        /// </summary>
        public override string ToString()
        {
            return string.Format("RCMD: {0}, Params: {1}", CommandName, Parameters.Count);
        }
    }
}
