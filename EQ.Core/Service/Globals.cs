using EQ.Core.Service;

namespace EQ.Core
{
    public static class Globals
    {
        // 사용법: L("Hello World")
        /// 문자 보간은 사용하면 안됨 => $"{xxx}"
        public static string L(string key)
        {
            if (ActManager.Instance?.Act?.Language == null) return key;
            return ActManager.Instance.Act.Language.GetText(key);
        }

        // 사용법: L("Error Code: {0}", 100)
        /// <summary>
        /// 문자 보간은 사용하면 안됨 => $"{xxx}"
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string L(string key, params object[] args)
        {
            string format = L(key);
            try { return string.Format(format, args); }
            catch { return format; }
        }
    }
}