using stenoapp.core.Interfaces;

using System.Reflection;

namespace stenoapp.core
{
    public class LoggerContext
    {
        private static ILogger Logger { get; set; }
            
        public static void Log(MethodBase methodBase, string message)
        {
            Logger.Log(methodBase, message);
        }
    }
}