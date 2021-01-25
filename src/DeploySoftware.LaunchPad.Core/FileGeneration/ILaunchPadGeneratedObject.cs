namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedObject
    {
        string Description { get; set; }
        string Id { get; set; }
        string IdType { get; set; }
        string Name { get; set; }
        string NamePrefix { get; set; }
        string NameSuffix { get; set; }
        string ObjectType { get; set; }
    }
}