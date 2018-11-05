using System;
using System.Diagnostics;
using System.Reflection;

namespace stenoapp.core
{
    public class InvocationContext
    {
        private static object lockObject = new object();

        private static InvocationContext _ctx;
        public static InvocationContext Instance
        {
            get
            {
                if (_ctx == null)
                {
                    lock (lockObject)
                    {
                        if (_ctx == null)
                        {
                            _ctx = new InvocationContext();
                        }
                    }
                }
                return _ctx;
            }
        }

        public IResponse Invoke<T>(string methodName, object instance, params object[] parameters)
        {
            MethodBase caller = new StackFrame(1).GetMethod();
            try
            {
                LoggerContext.Log(caller, "Entering...");

                MethodBase methodBase = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                object returnValue    = methodBase.Invoke(instance, parameters);

                return Response<T>.CreateSuccessResponse((T)returnValue);
            }
            catch (Exception ex)
            {
                LoggerContext.Log(caller, "Error: " + ex);

                return Response<T>.CreateFailureResponse(ex);
            }
            finally
            {
                LoggerContext.Log(caller, "Exiting...");
            }
        }
    }

    public interface IResponse
    {
        bool      Success   { get; set; }
        Exception Exception { get; set; }
    }

    public class Response<T> : IResponse
    {
        public T         Data      { get; set; }
        public bool      Success   { get; set; }
        public Exception Exception { get; set; }

        public static Response<T> CreateSuccessResponse(T data)
        {
            return new Response<T>
            {
                Data    = data,
                Success = true
            };
        }

        public static IResponse CreateFailureResponse(Exception ex)
        {
            return new Response<object>
            {
                Success   = false,
                Exception = ex
            };
        }
    }
}