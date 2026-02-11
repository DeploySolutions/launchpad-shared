using Deploy.LaunchPad.Util.Files.Formats;

namespace Deploy.LaunchPad.Util.Files
{
    public partial interface IMarkdownFile : IFile<string, MarkdownFileSchema>
    {
        public string Frontmatter { get; set; } //YAML/JSON/TOML
    }
}