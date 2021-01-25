namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedModule: ILaunchPadGeneratedObject
    {
        string Version { get; set; }
    }
}