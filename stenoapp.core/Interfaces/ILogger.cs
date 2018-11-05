using System.Reflection;

namespace stenoapp.core.Interfaces
{
    public interface ILogger
    {
        void Log(MethodBase methodBase, string logMessage);
    }
}