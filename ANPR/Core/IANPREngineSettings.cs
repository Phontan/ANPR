namespace Core
{
    public interface IANPREngineSettings
    {   
        int MinPlateHeight { get; set; }
        int MaxPlateHeight { get; set; }
        int MinPlateWidth { get; set; }
        int MaxPlateWidth { get; set; }
    }
}