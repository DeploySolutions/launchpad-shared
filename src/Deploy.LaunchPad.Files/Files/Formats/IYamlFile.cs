
using Deploy.LaunchPad.Files.Formats;

namespace Deploy.LaunchPad.Files
{
    /// <summary>
    /// Yaml (YAML)
    /// </summary>
    public partial interface IYamlFile : IFile<string, YamlFileSchema>
    {
    }
}