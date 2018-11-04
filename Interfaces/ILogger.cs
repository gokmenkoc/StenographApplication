using System.Reflection;

namespace stenoapp.Interfaces
{
    public interface ILogger
    {
        void Log(MethodBase methodBase, string logMessage);
    }
}