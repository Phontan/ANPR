using System;

namespace ANPR.Common.Log
{
    public interface ILogger
    {
        void Flush(TimeSpan? maxTimeToWait = null);

        void Fatal(string format, params object[] args);
        void Fatal(Exception exc, string format, params object[] args);
        void Error(string format, params object[] args);
        void Error(Exception exc, string format, params object[] args);
        void Info(string format, params object[] args);
        void Debug(string format, params object[] args);
    }
}
