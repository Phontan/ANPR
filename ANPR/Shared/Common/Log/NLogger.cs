using System;
using System.Linq;
using System.Threading;
using NLog;

namespace ANPR.Common.Log
{
    public class NLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        public void Flush(TimeSpan? maxTimeToWait = null)
        {
            var config = LogManager.Configuration;
            if (config == null)
                return;
            var asyncs = config.AllTargets.OfType<NLog.Targets.Wrappers.AsyncTargetWrapper>().ToArray();
            var countdown = new CountdownEvent(asyncs.Length);
            foreach (var wrapper in asyncs)
            {
                wrapper.Flush(x => countdown.Signal());
            }
            countdown.Wait(maxTimeToWait ?? TimeSpan.FromMilliseconds(500));
        }

        public void Fatal(string format, params object[] args)
        {
            _logger.Fatal(format, args);
        }

        public void Fatal(Exception exc, string format, params object[] args)
        {
            _logger.FatalException(string.Format(format, args), exc);
        }

        public void Error(string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        public void Error(Exception exc, string format, params object[] args)
        {
            _logger.ErrorException(string.Format(format, args), exc);
        }

        public void Info(string format, params object[] args)
        {
            _logger.Info(format, args);
        }

        public void Debug(string format, params object[] args)
        {
            _logger.Debug(format, args);
        }
    }
}
