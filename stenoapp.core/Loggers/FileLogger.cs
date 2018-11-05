using stenoapp.core.Interfaces;

using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace stenoapp.core.Loggers
{
    public class FileLogger : ILogger
    {
        public void Log(MethodBase methodBase, string message)
        {
            string logPath    = ConfigurationManager.AppSettings["LogFileDirectory"].TrimEnd('/');
            string logName    = DateTime.Now.ToString("yyyyMMdd_HHmm") + ".txt";
            string logMessage = string.Format("{0}.{1}.{2}: {3}",
                                    methodBase.ReflectedType.Namespace, methodBase.ReflectedType.Name, methodBase.Name, message);
            string path       = Path.Combine(logPath, logName);

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(logMessage);
            sw.Close();
            sw.Dispose();
        }
    }
}
