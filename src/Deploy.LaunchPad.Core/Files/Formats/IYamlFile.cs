using Deploy.LaunchPad.Core.Files.Formats;

namespace Deploy.LaunchPad.Core.Files
{
    /// <summary>
    /// Yaml (YAML)
    /// </summary>
    public partial interface IYamlFile : IFile<string, YamlFileSchema>
    {
    }
}