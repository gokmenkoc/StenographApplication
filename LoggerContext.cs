using stenoapp.Interfaces;

using System.Reflection;

namespace stenoapp
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