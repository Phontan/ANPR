using System;
using ANPR.Common;
using Core.Components;

namespace Core
{
    public delegate void FrameProcessingCompletedEventHandler(FrameProcessingCompletedEventHandlerArgs args);

    public interface IANPREngine : IDisposable
    {
        event FrameProcessingCompletedEventHandler FrameProcessingCompleted;
        IANPREngineSettings Settings { get; }
        void ScanImage(Image image);
    }
}