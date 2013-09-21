using System;
using System.Collections.Concurrent;
using System.Threading;
using ANPR.Common;
using ANPR.Common.Log;

namespace Core
{
    public class ANPREngine : IANPREngine
    {
        private static readonly ILogger Logger = new NLogger("ANPREngine");

        private readonly ConcurrentQueue<Image> _processingQueue;
        private readonly Thread _processingThread;
        private bool _isRunning;
        private readonly IANPREngineSettings _settings;

        public ANPREngine(IANPREngineSettings settings)
        {
            _settings = settings;
            _processingQueue = new ConcurrentQueue<Image>();
            _processingThread = new Thread(ProcessQueue);
            _isRunning = true;
            _processingThread.Start();
        }

        public event FrameProcessingCompletedEventHandler FrameProcessingCompleted;

        public IANPREngineSettings Settings { get { return _settings; } }

        public void ScanImage(Image image)
        {
            Ensure.NotNull(image, "image");

            _processingQueue.Enqueue(image);
        }

        private void ProcessQueue()
        {
            while (_isRunning)
            {
                try
                {
                    if (!_processingQueue.IsEmpty)
                    {
                        Image image;
                        if (_processingQueue.TryDequeue(out image))
                        {
                            //TODO:
                        }
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Error while processing item.");
                }
            }
        }

        public void Dispose()
        {
            _isRunning = false;
            if(_processingThread.IsAlive)
                _processingThread.Abort();
        }
    }
}
