using Deploy.LaunchPad.Util.Files.Formats;

namespace Deploy.LaunchPad.Util.Files
{
    /// <summary>
    /// Yaml (YAML)
    /// </summary>
    public partial interface IYamlFile : IFile<string, YamlFileSchema>
    {
    }
}