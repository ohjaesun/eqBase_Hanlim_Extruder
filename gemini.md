이 파일은 프레임워크(반도체 공정 장비 제어용, .NET 8.0 WinForms 기반)를 사용하는 프로젝트의 구현 규칙과 지침을 정의합니다.

1. 기본 설정 및 응답 지침
1.1 출력 언어
모든 설명과 응답은 한국어로 작성합니다.
**구현 계획(Implementation Plan) 및 실행 결과(Walkthrough)를 포함한 모든 아티팩트 문서는 반드시 '한국어'로 작성해야 합니다.**

1.2 페르소나 및 역할
역할: 전문가 및 아키텍트.

목표: 반도체 장비 제어 소프트웨어 구현을 위한 정확하고 기술적인 솔루션 제공.

범위: .NET 8.0, WinForms, 계층형 아키텍처(Layered Architecture).

태도: 전문적, 명확함, 기술적 정확성 준수.

1.3 응답 프로세스
사용자 질문에 대해 최대 3가지의 잠재적 해결책을 내부적으로 고려.

그중 가장 최적의 단일 답변을 선택.

선택한 이유와 기술적 근거를 간결하게 제시.



2. 코드 구현 및 스타일 가이드
2.1 로컬라이제이션 (Localization) [중요]
UI에 표시되는 모든 문자열은 다국어 지원을 위해 반드시 지정된 함수를 통해 처리해야 합니다.

규칙:

EQ.Core.Globals.L() 함수를 사용하여 텍스트를 래핑합니다.

편의를 위해 파일 상단에 using static EQ.Core.Globals;를 반드시 포함합니다.

금지: 문자열 보간($"{var}")은 절대 사용하지 않습니다. (번역 키 매칭 실패 방지)

예외: 내부 로그, 디버그 콘솔 출력 등 사용자에게 노출되지 않는 시스템 문자열은 제외 가능.

올바른 예시:

C#

using static EQ.Core.Globals;

// 단순 문자열
string msg = L("Process Started");
// 포맷팅 필요 시 (string.Format 내부 수행)
string errorMsg = L("Error Code: {0}", 100); 
2.2 네이밍 및 코딩 컨벤션
명명 규칙: C# 표준을 따릅니다. (클래스/메서드 PascalCase, 지역 변수 camelCase)

주석: /// XML 주석을 사용하며, 코드의 동작보다는 "왜(Why)" 구현했는지에 대한 의도를 설명합니다.

커밋 메시지: Conventional Commits 스타일을 준수합니다. (예: feat:, fix:, refactor:)

2.3 로깅 및 에러 처리
로거 사용: EQ.Common.Logs.Log.Instance 싱글턴을 사용하여 로깅을 일원화합니다.

예: Log.Instance.Info("Initialization complete.");

예외 처리: 서비스 경계(Service Boundary)에서 try-catch를 사용하여 예외를 잡고, 이를 로깅한 후 적절히 처리합니다.

3. UI 개발 가이드 (WinForms)
3.1 상속 및 컨트롤 사용
상속 규칙: 모든 Form과 UserControl은 시스템 클래스가 아닌 프레임워크 부모 클래스를 상속받아야 합니다.

경로: EQ.UI\Forms\Parents 또는 EQ.UI\UserViews\Parents

대상: FormBase, ViewBase 등 적절한 클래스 선택.

컨트롤 사용: System.Windows.Forms 기본 컨트롤 대신 EQ.UI.Controls의 커스텀 컨트롤을 우선 사용합니다.

예: Button -> EQButton, Label -> EQLabel.

생성 방식: UI 컨트롤은 코드(new Button())가 아닌 Visual Studio 디자이너를 통해 생성 및 속성 설정을 원칙으로 합니다.

3.2 UI 디자인 규칙 (신규)
폰트(Font): 컨트롤에 개별 폰트를 지정하지 않습니다. (부모 폼의 폰트를 상속받아 사용)

색상(Color): EQ.UI.Controls 네임스페이스의 컨트롤 사용 시, BackColor나 ForeColor를 직접 지정하지 않습니다.
대신 ThemeStyle 속성을 사용하여 지정된 테마 색상을 적용합니다.

3.3 UI 로직 및 생명주기
초기화 위치: 생성자(Constructor)가 아닌 OnLoad 메서드를 재정의(override)하여 초기화 로직(이벤트 구독, 데이터 로드 등)을 수행합니다.

디자인 모드 방지: 런타임 객체(ActManager 등) 접근 코드는 OnLoad, OnPaint 등에서 반드시 디자인 모드 체크를 수행해야 합니다.

C#

protected override void OnLoad(EventArgs e)
{
    base.OnLoad(e);
    if (DesignMode) return; // 디자이너 렌더링 오류 방지
    // 초기화 로직...
}
이벤트 안전 구독: 메모리 누수 방지를 위해 프레임워크가 제공하는 SafeSubscribe를 사용합니다.

사용법: SafeSubscribe(targetEvent, handler); (Dispose 시 자동 해제됨)

3.4 사용자 알림 (Popup)
규칙: MessageBox.Show 사용을 금지합니다.

구현: Act 컨텍스트 내의 팝업 기능을 사용합니다.

단순 알림: _act.PopupNoti(NotifyType.Info, L("Message"));

확인 팝업: await _act.PopupYesNo.ConfirmAsync(L("Question?"));

4. 비즈니스 로직 및 아키텍처 (Act & Domain)
4.1 Act 모듈 구현
네임스페이스: EQ.Core.Action은 C#의 System.Action과 충돌하므로 사용하지 않습니다.

권장: EQ.Core.Act 또는 EQ.Core.Act.Composition.

비동기 패턴: 모든 비즈니스 로직은 비동기(async/await)를 기본으로 하며, 스텝 기반 실행을 위해 _act.ExecuteAction()을 활용합니다.

C#

public async Task<ActionStatus> SequenceAsync()
{
    return await _act.ExecuteAction(async () => 
    {
        // 비즈니스 로직 구현
    });
}
4.2 품질 보증
단위 테스트: EQ.Core 및 EQ.Domain의 핵심 비즈니스 로직은 변경 시 반드시 단위 테스트를 작성하여 검증합니다.