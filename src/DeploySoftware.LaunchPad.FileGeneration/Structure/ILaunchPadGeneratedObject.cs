namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public interface ILaunchPadGeneratedObject
    {
        string Description { get; set; }
        string Id { get; set; }
        string IdType { get; set; }
        string Name { get; set; }
        string NamePrefix { get; set; }
        string NameSuffix { get; set; }
        string ObjectTypeName { get; set; }

        string ObjectTypeFullName { get; set; }

        string ObjectTypeAssemblyName { get; set; }
    }
}