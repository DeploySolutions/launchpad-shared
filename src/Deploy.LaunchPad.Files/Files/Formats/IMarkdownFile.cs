

using Deploy.LaunchPad.Files.Formats;

namespace Deploy.LaunchPad.Files
{
    public partial interface IMarkdownFile : IFile<string, MarkdownFileSchema>
    {
        public string Frontmatter { get; set; } //YAML/JSON/TOML
    }
}