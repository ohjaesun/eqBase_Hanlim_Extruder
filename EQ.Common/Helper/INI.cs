using System.Text;

namespace EQ.Common.Helper
{
    /// <summary>
    /// INI 파일 읽기/쓰기 클래스
    /// </summary>
    public class CIni
    {
        private readonly string m_FilePath;

        private static readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> _globalCache = new();
        private static readonly object _lock = new();

        private Dictionary<string, Dictionary<string, string>> GetIni()
        {
            lock (_lock)
            {
                if (!_globalCache.TryGetValue(m_FilePath, out var ini))
                {
                    ini = ReadAllLines();
                    _globalCache[m_FilePath] = ini;
                }

                return ini;
            }
        }

        public CIni(string fileName = "ModelData")
        {
            var _logRoot = Path.Combine(Directory.GetCurrentDirectory(), "ModelData");
            Directory.CreateDirectory(_logRoot);

            m_FilePath = Path.Combine(_logRoot, $"{fileName}.ini");
        }

        public void WriteString(string section, string key, string value)
        {
            var ini = GetIni();

            if (!ini.ContainsKey(section))
                ini[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            ini[section][key] = value;
            WriteAllLines(ini);
        }

        public void WriteInteger(string section, string key, int value)
        {
            WriteString(section, key, value.ToString());
        }

        public void WriteFloat(string section, string key, float value)
        {
            WriteString(section, key, value.ToString());
        }

        public void WriteBool(string section, string key, bool value)
        {
            WriteString(section, key, value.ToString());
        }

        public string ReadString(string section, string key, string defaultValue = "")
        {
            var ini = GetIni();
            if (ini.TryGetValue(section, out var sectionDict) && sectionDict.TryGetValue(key, out var value))
                return value;

            return defaultValue;
        }

        public int ReadInteger(string section, string key, int defaultValue = 0)
        {
            if (int.TryParse(ReadString(section, key), out int result))
                return result;
            return defaultValue;
        }

        public float ReadFloat(string section, string key, float defaultValue = 0f)
        {
            if (float.TryParse(ReadString(section, key), out float result))
                return result;
            return defaultValue;
        }

        public bool ReadBool(string section, string key, bool defaultValue = false)
        {
            if (bool.TryParse(ReadString(section, key), out bool result))
                return result;
            return defaultValue;
        }

        public string[] GetSectionNames()
        {
            var ini = GetIni();
            return ini.Keys.ToArray();
        }

        public string[] GetEntryNames(string section)
        {
            var ini = GetIni();
            return ini.TryGetValue(section, out var dict) ? dict.Keys.ToArray() : Array.Empty<string>();
        }

        public void DeleteSection(string section)
        {
            var ini = GetIni();
            if (ini.ContainsKey(section))
            {
                ini.Remove(section);
                WriteAllLines(ini);
            }
        }

        // Load INI into memory
        private Dictionary<string, Dictionary<string, string>> ReadAllLines()
        {
            var ini = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

            try
            {
                if (!File.Exists(m_FilePath))
                    return ini;

                string currentSection = "";

                foreach (var line in File.ReadAllLines(m_FilePath, Encoding.UTF8))
                {
                    var trimmed = line.Trim();
                    if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith(";"))
                        continue;

                    if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                    {
                        currentSection = trimmed.Substring(1, trimmed.Length - 2);
                        if (!ini.ContainsKey(currentSection))
                            ini[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    }
                    else if (trimmed.Contains('=') && !string.IsNullOrEmpty(currentSection))
                    {
                        var idx = trimmed.IndexOf('=');
                        var key = trimmed.Substring(0, idx).Trim();
                        var val = trimmed.Substring(idx + 1).Trim();
                        ini[currentSection][key] = val;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return ini;
        }

        // Save INI sorted
        private void WriteAllLines(Dictionary<string, Dictionary<string, string>> ini)
        {
            var sb = new StringBuilder();

            foreach (var section in ini.Keys.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
            {
                sb.AppendLine($"[{section}]");

                foreach (var key in ini[section].Keys.OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
                {
                    sb.AppendLine($"{key}={ini[section][key]}");
                }

                sb.AppendLine(); // 섹션 구분
            }

            File.WriteAllText(m_FilePath, sb.ToString(), new UTF8Encoding(false)); // BOM 없는 UTF-8 저장
        }
    }
}
#if false  // Ansi 버전은 한글 지원이 안됨
using System.Runtime.InteropServices;
using System.Text;

namespace CINI
{
    public class CIni
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, int Key,
               string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result,
               int Size, string FileName);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(int Section, string Key,
               string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result,
               int Size, string FileName);


        private string m_FilePath;
        public CIni()
        {
            var _logRoot = Directory.GetCurrentDirectory() + "\\ModelData\\";

            Directory.CreateDirectory(_logRoot);


            DateTime _dtNow = DateTime.Now;
            // Log file path
            m_FilePath = $"{_logRoot}\\ModelData.ini";
        }
        public CIni(string FilePath)
        {
            var _logRoot = Directory.GetCurrentDirectory() + "\\ModelData\\";


            m_FilePath = $"{_logRoot}\\{FilePath}.ini";
        }

        public string[] GetSectionNames()  // ini 파일 안의 모든 section 이름 가져오기
        {
            for (int maxsize = 500; true; maxsize *= 2)
            {
                byte[] bytes = new byte[maxsize];
                int size = GetPrivateProfileString(0, "", "", bytes, maxsize, m_FilePath);


                if (size < maxsize - 2)
                {
                    string Selected = Encoding.ASCII.GetString(bytes, 0, size - (size > 0 ? 1 : 0));
                    return Selected.Split(new char[] { '\0' });
                }
            }
        }
       
        public string[] GetEntryNames(string section)   // 해당 section 안의 모든 키 값 가져오기
        {
            for (int maxsize = 500; true; maxsize *= 2)
            {
                byte[] bytes = new byte[maxsize];
                int size = GetPrivateProfileString(section, 0, "", bytes, maxsize, m_FilePath);

                if (size < maxsize - 2)
                {
                    string entries = Encoding.GetEncoding(949).GetString(bytes, 0, size - (size > 0 ? 1 : 0));
                    //  string entries = Encoding.UTF8.GetString(bytes, 0, size - (size > 0 ? 1 : 0));
                    //  string entries = Encoding.Default.GetString(bytes, 0, size - (size > 0 ? 1 : 0));
                    //     string entries = Encoding.ASCII.GetString(bytes, 0, size - (size > 0 ? 1 : 0));
                    return entries.Split(new char[] { '\0' });
                }
            }
        }

        public void DeleteSection(string section)
        {
            //  long result = 0;

            WritePrivateProfileString(section, null, null, m_FilePath);
        }

        public long WriteInteger(string section, string key, int Value)
        {
            long result = 0;

            result = WritePrivateProfileString(section, key, Value.ToString(), m_FilePath);

            return result;
        }

        public long WriteFloat(string section, string key, float Value)
        {
            long result = 0;

            result = WritePrivateProfileString(section, key, Value.ToString(), m_FilePath);

            return result;
        }

        public long WriteString(string section, string key, string Value)
        {
            long result = 0;

            result = WritePrivateProfileString(section, key, Value, m_FilePath);

            return result;
        }

        public long WriteBool(string section, string key, bool Value)
        {
            long result = 0;

            result = WritePrivateProfileString(section, key, Value.ToString(), m_FilePath);

            return result;
        }

        public bool ReadBool(string section, string key, bool DefalutValue)
        {
            bool result = DefalutValue;
            int retcount;

            StringBuilder retValue = new StringBuilder(100);

            retcount = GetPrivateProfileString(section, key, "", retValue, 100, m_FilePath);
            if (retcount > 0)
            {
                result = bool.Parse(retValue.ToString());
            }

            return result;
        }

        public int ReadInteger(string section, string key, int DefalutValue)
        {
            int result = DefalutValue, retcount;
            StringBuilder retValue = new StringBuilder(100);

            retcount = GetPrivateProfileString(section, key, "", retValue, 100, m_FilePath);
            if (retcount > 0)
            {
                result = int.Parse(retValue.ToString());
            }

            return result;
        }

        public float ReadFloat(string section, string key, float DefalutValue)
        {
            float result = DefalutValue;
            int retcount;
            StringBuilder retValue = new StringBuilder(100);

            retcount = GetPrivateProfileString(section, key, "", retValue, 100, m_FilePath);
            if (retcount > 0)
            {
                result = float.Parse(retValue.ToString());
            }

            return result;
        }

        public string ReadString(string section, string key, string DefalutValue)
        {

            string result = DefalutValue;
            int retcount;
            StringBuilder retValue = new StringBuilder(200);

            retcount = GetPrivateProfileString(section, key, "", retValue, 200, m_FilePath);
            if (retcount > 0)
            {
                result = retValue.ToString();
            }

            return result;

#if false
            char[] bytes = new char[100];
            int size = GetPrivateProfileString(section, key, "", bytes, 100, m_FilePath);

            Encoding euckr = Encoding.GetEncoding(51949);

            byte[] byte_a = Encoding.UTF8.GetBytes((bytes); string str_b = Encoding.UTF8.GetString(byte_a);

       

            if (size < 100 - 2)
            {
                string entries = Encoding.UTF8.GetString(bytes, 0, size );
                return entries; // entries.Split(new char[] { '\0' });
            }
            return DefalutValue;
#endif
        }
    }
}
#endif