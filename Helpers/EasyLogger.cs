using System;
using System.IO;
using System.Text;

namespace backend.Helpers
{
    public sealed class EasyLogger
    {
        private static EasyLogger? _instance;
        private static readonly object _instanceLock = new object();

        private readonly object _writeLock = new object();
        private readonly string _logFilePath;

        private EasyLogger()
        {
            _logFilePath = LogFilePath();
            var directory = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static EasyLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new EasyLogger();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Log(string fileName, string functionName, string message)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var line = $"{timestamp} | {fileName} | {functionName} | {message}";

            lock (_writeLock)
            {
                File.AppendAllText(_logFilePath, line + Environment.NewLine, Encoding.UTF8);
            }
        }

        private static string LogFilePath()
        {
            var current = new DirectoryInfo(AppContext.BaseDirectory);
            while (current != null)
            {
                if (string.Equals(current.Name, "backend"))
                {
                    return Path.Combine(current.FullName, "app.log");
                }
                current = current.Parent;
            }
            return Path.Combine(AppContext.BaseDirectory, "app.log");
        }
    }
}


