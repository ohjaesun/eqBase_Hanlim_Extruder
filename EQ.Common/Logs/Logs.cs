using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace EQ.Common.Logs
{
    /// <summary>
    /// Thread-safe Log file writer
    /// </summary>
    public sealed class Log
    {
        #region Fields
        private static Log _instance = null; // Singleton instance
        static readonly object _locker = new object();  // file write lock
        private static readonly object _lock = new object();

        private DateTime _dtNow;    // Current datetime
        private string _logRoot = null;    // Log folder root path
        private string _fullPath = null;    // Current log file path
        private string _eachPath = null;
        private Stopwatch _updateStopWatch = new Stopwatch();

        private string preString = "";

        private ConcurrentQueue<logData> queue = new ConcurrentQueue<logData>();

        //  private bool[] IsWriteSkip = new bool[Enum.GetNames(typeof(LogType)).Length];

        public delegate void Msg(string result);
        public event Msg OnMsg;

        private struct logData
        {
            public LogType logtype;
            public string filePath;
            public string fileLine;
            public string callerName;
            public string time;
            public string log;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Singleton implementation
        /// </summary>
        public static Log Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new Log();
                    }
                }
                return _instance;
            }

            /* 싱글톤인대 2번 이상 생성자 호출되는 경우 있음
            get
            {
                LazyInitializer.EnsureInitialized(ref _instance, () => new Log());
                return _instance;
            }
            */
        }
        #endregion

        #region Public Methods

        public int GetLogCount()
        {
            return queue.Count;
        }

        public void StopwatchStart()
        {
            _updateStopWatch.Reset();
            _updateStopWatch.Start();
        }
        public long StopwatchStop()
        {
            //    _updateStopWatch.Stop();
            return _updateStopWatch.ElapsedMilliseconds;
        }

        bool isWriteFileInfo = true;
        public void SetWriteFileInfo(bool Write)
        {
            isWriteFileInfo = Write;
        }

        public enum LogType
        {
            Debug,
            Info,
            Warning,
            Error,
            Time,
            SecsGem,
            Sequence,
            Action,
            Controls,
            ChipDataChange,
            SaveData
        }

        public void SaveData(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.SaveData}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";


            logData _d = new logData();
            _d.logtype = LogType.SaveData;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }

        public void ChipDataChange(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.ChipDataChange}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";


            logData _d = new logData();
            _d.logtype = LogType.ChipDataChange;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }
        public void Controls(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.Controls}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";


            logData _d = new logData();
            _d.logtype = LogType.Controls;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }
        public void Sequence(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.Sequence}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";


            logData _d = new logData();
            _d.logtype = LogType.Sequence;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }
        public void Action(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.Action}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";


            logData _d = new logData();
            _d.logtype = LogType.Action;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }
        public void Debug(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.Debug}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";


            logData _d = new logData();
            _d.logtype = LogType.Debug;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }

        public void Warning(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.Warning}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";

            logData _d = new logData();
            _d.logtype = LogType.Warning;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }

        /// <summary>
        /// 별도 Alarm 폴더에 기록 됨
        /// </summary>
        /// <param name="content"></param>
        /// <param name="callerName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        public void Alarm(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Directory.CreateDirectory(_logRoot);
            Directory.CreateDirectory($"{_logRoot}\\Alarm");

            var fileName = DateTime.Now.ToString("yyyyMMdd");
            var path = $"{_logRoot}\\Alarm\\{fileName}.txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            { writer.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")},{DateTime.Now.ToOADate()},{content}"); }
        }

        public void Error(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.Error}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";

            logData _d = new logData();
            _d.logtype = LogType.Error;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }

        public void Info(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.Info}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";

            logData _d = new logData();
            _d.logtype = LogType.Info;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }

        public void SecsGem(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.SecsGem}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss:fff")}],{content}";

            logData _d = new logData();
            _d.logtype = LogType.SecsGem;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }


        public void Time(string content, [CallerMemberName] string callerName = null, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (_dtNow.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                ChangeDay();
            }

            if (preString == content) return;
            else preString = content;

            string fullContents = $"[{LogType.Time}],[{callerName}],[{DateTime.Now.ToString("HH:mm:ss.fff")}],{content}";

            logData _d = new logData();
            _d.logtype = LogType.Time;
            _d.callerName = callerName;
            _d.filePath = sourceFilePath;
            _d.fileLine = sourceLineNumber.ToString();
            _d.time = DateTime.Now.ToString("HH:mm:ss:fff");
            _d.log = content;


            queue.Enqueue(_d);

            return;
        }
        #endregion

        #region Private Methods

        int memoryCount = 0;
        /// <summary>
        /// Creator
        /// </summary>
        private Log()
        {
            _logRoot = Directory.GetCurrentDirectory() + "\\Log\\";

            ChangeDay();
        }

        Task ThreadRun;

        /// <summary>
        /// New day checker
        /// </summary>
        private void ChangeDay()
        {
            _dtNow = DateTime.Now;

            // Folder check
            Directory.CreateDirectory(_logRoot);
            Directory.CreateDirectory($"{_logRoot}\\{_dtNow.Year.ToString()}");
            Directory.CreateDirectory($"{_logRoot}\\{_dtNow.Year.ToString()}\\{_dtNow.ToString("MM")}");
            Directory.CreateDirectory($"{_logRoot}\\{_dtNow.Year.ToString()}\\{_dtNow.ToString("MM")}\\{_dtNow.ToString("dd")}");

            // Log file path
            _fullPath = $"{_logRoot}\\{_dtNow.Year.ToString()}\\{_dtNow.ToString("MM")}\\{_dtNow.ToString("dd")}\\Log.txt";
            _eachPath = $"{_logRoot}\\{_dtNow.Year.ToString()}\\{_dtNow.ToString("MM")}\\{_dtNow.ToString("dd")}\\";

            LogDelete();

            //실제 로그 파일 기록 부분

            if (ThreadRun == null || ThreadRun.Status == TaskStatus.Canceled || ThreadRun.Status == TaskStatus.Faulted)
            {
                ThreadRun = new Task(async () =>
                {
                    Dictionary<LogType, StringBuilder> log_dic = new Dictionary<LogType, StringBuilder>();
                    StringBuilder allLog = new StringBuilder();
                    Stopwatch sw = new Stopwatch();

                    int delayCount = 0;

                    while (true)
                    {
                        log_dic.Clear();
                        allLog.Clear();

                        if (!queue.IsEmpty)
                        {
                            delayCount = 1;

                            sw.Restart();

                            var logBuffer = new List<logData>();

                            while (queue.TryDequeue(out logData data))
                            {
                                logBuffer.Add(data);

                                if (sw.ElapsedMilliseconds > 1000)
                                    break;
                                if (queue.IsEmpty)
                                    break;
                            }

                            foreach (var data in logBuffer)
                            {
                                if (!log_dic.ContainsKey(data.logtype))
                                    log_dic[data.logtype] = new StringBuilder();

                                var filePath = isWriteFileInfo ? $"[{Path.GetFileName(data.filePath)}({data.fileLine})]," : "";
                                var _log = $"[{data.logtype}],[{data.time}],[{data.callerName}],{filePath} {data.log}";

                                //Log.txt에 기록 
                                {
                                    if (data.logtype == LogType.Time) { } //time은 전체 로그에 기록 안함
                                    else if (data.logtype == LogType.SaveData) // SaveData 는 첫줄만 기록
                                    {
                                        //첫줄만 기록
                                        var lines = _log.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                                        allLog.AppendLine(lines[0]);
                                    }
                                    else // 위에것 빼고는 전체 로그에 남김
                                        allLog.AppendLine(_log);
                                }

                                log_dic[data.logtype].AppendLine(_log);

                                OnMsg?.Invoke(_log);
                            }

                            using (var writer = new StreamWriter(_fullPath, true))
                            {
                                await writer.WriteAsync(allLog.ToString());
                            }


                            foreach (var p in log_dic)
                            {
                                string filePath = Path.Combine(_eachPath, $"{p.Key}.txt");
                                using (var writer = new StreamWriter(filePath, true))
                                {
                                    await writer.WriteAsync(p.Value.ToString());
                                }
                            }

                            logBuffer.Clear();
                        }


                        int delayTime = Math.Min(delayCount * 20, 5000);
                        await Task.Delay(delayTime);
                        delayCount++;
                    }

                }, TaskCreationOptions.LongRunning);
                ThreadRun.Start();
            }
        }

        public void LogDelete()
        {
            // 로그 파일 자동 삭제하는 스레드
            ThreadPool.QueueUserWorkItem(__ =>
            {
                try
                {
                    int periodDay = 60; //2Month
                    var expireDate = DateTime.Now.AddDays(-periodDay);
                    var expireDateExcption = DateTime.Now.AddDays(-7); // dmp 용량 때문에 7일로 하자

                    var path = Directory.GetCurrentDirectory() + "\\Log\\";
                    var deletePathList = new List<string>();
                    var rootdirectory = path;
                    var isFinished = false;

#if true
                    var delCount = 0;
                    var logFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                    foreach (var file in logFiles)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(file);

                            if (fi.CreationTime < DateTime.Now.AddDays(-periodDay) || fi.LastWriteTime < DateTime.Now.AddDays(-periodDay))
                            {
                                fi.Delete();
                                delCount++;
                            }
                        }
                        catch (Exception ex)
                        {
                            //  Console.WriteLine($"파일 삭제 실패: {file}, 이유: {ex.Message}");
                        }
                    }
                    Log.Instance.Info($"[Log] {delCount}개 파일 삭제");
#else
                    new string[] { "yyyy", "MM", "dd" }.ToList().ForEach(dateStr =>
                    {
                        if (isFinished == false && Directory.Exists(rootdirectory))
                        {
                            var directories = Directory.GetDirectories(rootdirectory);

                            var directoriesFiltered = directories.Where(dirPath =>
                            {
                                try
                                {
                                    string directoryName = Path.GetFileName(dirPath);
                                    if (int.TryParse(directoryName, out int directoryNameInt) == false)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return directoryNameInt < int.Parse(expireDate.ToString(dateStr));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Instance.Error($"[Exception] Directory filter. (Msg) :{ex.Message}\r\n{ex.StackTrace}");
                                    return false;
                                }
                            });

                            if (directoriesFiltered.Count() >= 1)
                            {
                                deletePathList.AddRange(directoriesFiltered);
                            }
                            rootdirectory = $@"{rootdirectory}\{expireDate.ToString(dateStr)}";
                        }
                        else
                        {
                            isFinished = true;
                        }
                    });

                    deletePathList.ForEach(dirPath =>
                    {
                        try
                        {
                            Directory.Delete(dirPath, true);
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[Exception] Unknown Exception. (Msg) :{ex.Message}\r\n{ex.StackTrace}");
                        }
                    });


                    //exception folder
                    path = Directory.GetCurrentDirectory() + "\\Log\\Exception\\";
                    var files = Directory.GetFiles(path);
                    foreach (var file in files)
                    {
                        try
                        {
                            DateTime creationTime = File.GetCreationTime(file);
                            if (creationTime < expireDateExcption)
                            {
                                File.Delete(file);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[Exception] 파일 삭제 실패: {file} (Error: {ex.Message})\n{ex.StackTrace}");
                        }
                    }

                    //SecsGem folder
                    path = Directory.GetCurrentDirectory() + "\\Log\\SecsGem\\";
                    var folderNames = Directory.GetDirectories(path)
                                .Select(dirPath => Path.GetFileName(dirPath))
                                .ToList();

                    foreach (var folderName in folderNames)
                    {
                        try
                        {
                            if (int.Parse(folderName) < int.Parse(expireDate.ToString("yyyyMMdd")))
                            {
                                Directory.Delete(path + folderName, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[SecsGem] 파일 삭제 실패: {folderName} (Error: {ex.Message})\n{ex.StackTrace}");
                        }
                    }

                    //AlarmCount folder
                    path = Directory.GetCurrentDirectory() + "\\Log\\Alarm\\";
                    folderNames = Directory.GetDirectories(path)
                                .Select(dirPath => Path.GetFileName(dirPath))
                                .ToList();

                    foreach (var folderName in folderNames)
                    {
                        try
                        {
                            if (int.Parse(folderName) < int.Parse(expireDate.ToString("yyyyMMdd")))
                            {
                                Directory.Delete(path + folderName, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[AlarmCount] 파일 삭제 실패: {folderName} (Error: {ex.Message})\n{ex.StackTrace}");
                        }
                    }


#endif
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[Exception] Delete File {ex.Message}\r\n{ex.StackTrace}");
                    return;
                }
                finally
                {

                }

            });
        }
        #endregion
    }
}
