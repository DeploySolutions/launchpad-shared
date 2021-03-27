namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedComponent<TComponentConfig> : ILaunchPadGeneratedObject
        where TComponentConfig : LaunchPadGeneratedConfiguration, new()
    {
        public TComponentConfig Config {get;set;}

        string EntityIdType { get; set; }
    }
}