namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedComponent : ILaunchPadGeneratedObject
    {
        string EntityIdType { get; set; }
        string Version { get; set; }
    }
}