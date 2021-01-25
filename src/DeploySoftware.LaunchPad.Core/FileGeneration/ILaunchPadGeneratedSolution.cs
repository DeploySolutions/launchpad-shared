namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedSolution : ILaunchPadGeneratedObject
    {
        string Version { get; set; }
    }
}