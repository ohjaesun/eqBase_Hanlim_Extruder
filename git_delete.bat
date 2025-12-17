@echo off
chcp 65001 > nul
cd /d %~dp0
echo 현재 위치:
cd

:: 1. 삭제될 파일 미리보기 (Dry Run)
echo ==============================================
echo [미리보기] 아래 파일 및 폴더가 삭제될 예정입니다:
echo ==============================================
git clean -nfd

echo.
echo.

:: 2. 사용자 확인 (Y/N)
set /p user_input=위 목록의 파일들을 정말로 삭제하시겠습니까? (Y를 누르면 삭제): 

:: 3. 입력값 확인 (대소문자 구분 없이 Y일 때만 실행)
if /i "%user_input%"=="Y" (
    echo.
    echo 삭제를 진행합니다...
    git clean -fd
    echo.
    echo [완료] 삭제되었습니다.
) else (
    echo.
    echo [취소] 작업이 취소되었습니다. 아무것도 삭제되지 않았습니다.
)

:: 창이 바로 닫히지 않도록 대기
pause
