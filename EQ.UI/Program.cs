using EQ.Core.Act;
using EQ.Core.Service;
using EQ.UI;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EQ
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(exceptionDump);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            bool bRun;
            Mutex mutex;// = new Mutex(true, "INNOBIZ_HANDLER", out bRun);

            if (args.Length > 0)
            {
                mutex = new Mutex(true, $"INNOBIZ_HANDLER_{args[0]}", out bRun);
            }
            else
            {
                mutex = new Mutex(true, "INNOBIZ_HANDLER", out bRun);
            }

            if (!bRun)
            {
                MessageBox.Show("이중 실행 , 종료 합니다.");
                Application.Exit();
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormSplash());
        }

        static void exceptionDump(object sender, System.Threading.ThreadExceptionEventArgs args)
        {
            string message = "\n";
            Exception exc = (Exception)args.Exception;

            string call_stacks = exc.StackTrace.ToString();

            message += "Error: " + exc.Message + "\n";
            message += "Loction: " + call_stacks;

            var rcpPath = Directory.GetCurrentDirectory() + "\\Log\\Exception";
            var di = new DirectoryInfo(rcpPath);
            if (!di.Exists) { di.Create(); }

            using (StreamWriter sw = new StreamWriter(rcpPath + $"\\Exception{DateTime.Now.ToString("yyyyMMdd")}.txt", true))
            {
                sw.WriteLine($"START , ThreadException");
                sw.WriteLine($"{DateTime.Now.ToString("F")} {message}");

                sw.WriteLine($"ALL TRACE");
                string stackStr = "";
                StackTrace stackTrace = new StackTrace(true);
                foreach (var frame in stackTrace.GetFrames())
                {
                    var method = frame.GetMethod();
                    var fullFileName = frame.GetFileName();
                    var fileName = fullFileName != null ? Path.GetFileName(fullFileName) : "Unknown";
                    var lineNumber = frame.GetFileLineNumber();
                    stackStr += $"{method.DeclaringType.FullName}.{method.Name} , [{fileName},{lineNumber}]\n";
                }
                sw.WriteLine(stackStr);
                sw.WriteLine($"END");
            }

            ActManager.Instance.Act.AuditTrail.RecordSystemCrash();

            CreateMiniDump(rcpPath + $"\\Exception{DateTime.Now.ToString("yyyyMMdd_ff")}.dmp");
        }

        //이벤트 클래스(처리되지 않은 예외)
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string message = "\n";
            Exception exc = (Exception)e.ExceptionObject;

            string call_stacks = exc.StackTrace.ToString();

            message += "Error: " + exc.Message + "\n";
            message += "Loction: " + call_stacks;

            var rcpPath = Directory.GetCurrentDirectory() + "\\Log\\Exception";
            var di = new DirectoryInfo(rcpPath);
            if (!di.Exists) { di.Create(); }

            using (StreamWriter sw = new StreamWriter(rcpPath + $"\\Exception{DateTime.Now.ToString("yyyyMMdd")}.txt", true))
            {
                sw.WriteLine($"START , UnhandledException");
                sw.WriteLine($"{DateTime.Now.ToString("F")} {message}");

                sw.WriteLine($"ALL TRACE");
                string stackStr = "";
                StackTrace stackTrace = new StackTrace(true);
                foreach (var frame in stackTrace.GetFrames())
                {
                    var method = frame.GetMethod();
                    var fullFileName = frame.GetFileName();
                    var fileName = fullFileName != null ? Path.GetFileName(fullFileName) : "Unknown";
                    var lineNumber = frame.GetFileLineNumber();
                    stackStr += $"{method.DeclaringType.FullName}.{method.Name} , [{fileName},{lineNumber}]\n";
                }
                sw.WriteLine(stackStr);
                sw.WriteLine($"END");
            }
            ActManager.Instance.Act.AuditTrail.RecordSystemCrash();
            CreateMiniDump(rcpPath + $"\\Exception{DateTime.Now.ToString("yyyyMMdd_ff")}.dmp");
        }

        [DllImport("dbghelp.dll", SetLastError = true)]
        static extern bool MiniDumpWriteDump(
      IntPtr hProcess,
      uint processId,
      IntPtr hFile,
      MINIDUMP_TYPE dumpType,
      IntPtr exceptionParam,
      IntPtr userStreamParam,
      IntPtr callbackParam
  );
        enum MINIDUMP_TYPE : uint
        {
            MiniDumpNormal = 0x00000000,
            MiniDumpWithDataSegs = 0x00000001,
            MiniDumpWithFullMemory = 0x00000002,
            MiniDumpWithHandleData = 0x00000004,
            MiniDumpFilterMemory = 0x00000008,
            MiniDumpScanMemory = 0x00000010,
            MiniDumpWithUnloadedModules = 0x00000020,
            MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
            MiniDumpFilterModulePaths = 0x00000080,
            MiniDumpWithProcessThreadData = 0x00000100,
            MiniDumpWithPrivateReadWriteMemory = 0x00000200,
            MiniDumpWithoutOptionalData = 0x00000400,
            MiniDumpWithFullMemoryInfo = 0x00000800,
            MiniDumpWithThreadInfo = 0x00001000,
            MiniDumpWithCodeSegs = 0x00002000
        }

        static void CreateMiniDump(string dumpFilePath)
        {
            if (true)
            {
                using (Process process = Process.GetCurrentProcess())
                {
                    using (FileStream fs = new FileStream(dumpFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {

                        bool success = MiniDumpWriteDump(
                            process.Handle,
                            (uint)process.Id,
                            fs.SafeFileHandle.DangerousGetHandle(),
                            MINIDUMP_TYPE.MiniDumpWithFullMemory, // full dump
                            IntPtr.Zero,
                            IntPtr.Zero,
                            IntPtr.Zero
                        );

                        MessageBox.Show("프로그램 예외 발생 및 덤프 파일 생성 !");
                    }
                }
            }

        }
    }
}