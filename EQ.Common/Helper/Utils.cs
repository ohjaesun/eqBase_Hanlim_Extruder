using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EQ.Common.Helper
{
    public static class Utils
    {
        public static int GetButtonIdx(string buttonName)
        {

            string BtnIdxText = Regex.Replace(buttonName, @"\D", "");
            int BtnIdx = string.IsNullOrEmpty(BtnIdxText) ? -1 : int.Parse(BtnIdxText);

            return BtnIdx;
        }

        public static void MakeFolder(string path)
        {
            DirectoryInfo di;

            if (path.Contains(":")) //full path 
            {
                di = new DirectoryInfo(path);
                if (!di.Exists) { di.Create(); }
            }
            else
            {
                string rootPath = Environment.CurrentDirectory + $"\\{path}";
                di = new DirectoryInfo(rootPath);
                if (!di.Exists) { di.Create(); }
            }
        }
    }
}
