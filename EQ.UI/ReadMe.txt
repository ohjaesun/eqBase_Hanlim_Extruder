핵심 규칙 요약
의존성 방향: UI → Core → Domain → Common

예외 (의존성 역전): Core는 Infra를 모릅니다. 대신 Core와 Infra는 둘 다 Domain에 정의된 **인터페이스(계약서)**를 바라봅니다.

조립자: UI 프로젝트가 모든 것을 조립합니다. UI가 Infra의 WmxIoController를 생성(new)해서 Core의 ActManager에게 전달(주입)합니다.

------------------------------------------------------------------

EQ.UI (시작 프로젝트/얼굴)
역할: 프로그램의 시작점. 모든 부품을 조립(Composition Root)하고 Core의 서비스를 호출

참조:
Common
EQ.Domain (UI에 데이터를 표시하기 위해)
EQ.Core (시퀀스 실행 등 서비스 호출을 위해)
EQ.Infra (Infra의 팩토리(예: IoFactory)를 호출하여 실제 하드웨어 객체를 생성하고, 이것을 Core에 주입하기 위해)

------------------------------------------------------------------
EQ.Infra (손발)
역할: Domain에 정의된 인터페이스(예: IIoController)를 실제 하드웨어로 구현 (예: WmxIoController)

참조:
Common
EQ.Domain

------------------------------------------------------------------
EQ.Core (두뇌)
역할: Domain의 인터페이스를 사용하여 실제 응용 로직(시퀀스, 액션)을 처리

참조:
Common
EQ.Domain

(중요) EQ.Infra를 절대로 참조해서는 안 됩니다.

------------------------------------------------------------------
EQ.Domain (핵심)
역할: 비즈니스 규칙, 데이터 모델(엔티티), 그리고 Infra가 구현할 인터페이스 (예: IIoController, IMotionController) 정의

참조:
Common

------------------------------------------------------------------

Common (공구함)
역할: 모든 프로젝트가 공통으로 사용하는 유틸리티 (로깅 등)

참조: 없음 (다른 솔루션 프로젝트를 참조하지 않아야 합니다.)

------------------------------------------------------------------
Hardware.Infra.XXX.YYY (외부 종속성)  프로젝트 구성에서 제외 해도 무방 ( DLL만 복사해서 사용 )

-csproj 파일에 출력 파일 복사 설정 필요 (아래 예시 참고 : 동적 로딩 DLL은 자동으로 복사되지 않음)
<Exec Command="xcopy &quot;$(TargetDir)*.*&quot; &quot;$(SolutionDir)..\Excute\EQ.UI\$(Platform)\$(Configuration)\&quot; /e /y /i /d" />

역할: 실제 하드웨어 제조사에서 제공하는 SDK, DLL 등 외부 종속성
	  Infra 프로젝트에 구현되어야 하는 내용이지만 직접 참조하지 않고 Infra 프로젝트에서 어댑터 패턴 등을 사용하여 간접 참조
	  모터 상위제어기를 WMX , Ajin등 사용한다 가정할때 필요한 DLL만 참조하기 위해

참조:
Common
EQ.Domain

