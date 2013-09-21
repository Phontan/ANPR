namespace Core.Components
{
    public class FrameProcessingCompletedEventHandlerArgs
    {
        public readonly bool Success;

        public FrameProcessingCompletedEventHandlerArgs(bool success)
        {
            Success = success;
        }
    }
}