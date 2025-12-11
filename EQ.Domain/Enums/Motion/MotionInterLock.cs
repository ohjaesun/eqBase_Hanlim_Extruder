using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Enums
{   
    public enum InterLockType
    {
        Position,       // 다른 축의 현재 위치 비교
        DefinedPos,     // 정의된 포지션(이름) 비교
        IoInput,        // Input 신호 상태
        IoOutput        // Output 신호 상태
    }

    public enum CompareCondition
    {
        Less,       // < (작으면)
        Greater,    // > (크면)
        Equal,      // == (같으면, 범위 내)
        NotEqual    // != (다르면, 범위 밖)
    }

    public enum StopDirection
    {
        Both,       // 양방향 금지
        Positive,   // (+) 방향 금지
        Negative    // (-) 방향 금지
    }
}
