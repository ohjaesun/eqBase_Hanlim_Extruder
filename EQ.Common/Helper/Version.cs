using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Common.Helper
{
    public static class VersionHelper
    {
        public static string GetDisplayVersion()
        {
            // 1. 어셈블리 버전 가져오기 (Project Properties에서 설정한 값)
            Version version = Assembly.GetEntryAssembly().GetName().Version;

            // 2. 빌드 날짜 (파일 수정 날짜 기준 - 간편 방식)
            string location = Assembly.GetEntryAssembly().Location;
            DateTime buildDate = File.GetLastWriteTime(location);

            // 포맷: Ver 1.0.0 (241121)
            //return $"Ver{version.Major}.{version.Minor}.{version.Build}({buildDate:yyMMdd})";

            //날짜만 보여주자
            return $"Ver:{buildDate:yyMMdd}";
        }
    }
}
