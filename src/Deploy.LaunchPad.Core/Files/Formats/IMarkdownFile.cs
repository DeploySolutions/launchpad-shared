using Deploy.LaunchPad.Core.Files.Formats;

namespace Deploy.LaunchPad.Core.Files
{
    public partial interface IMarkdownFile : IFile<string, MarkdownSchema>
    {
        public string Frontmatter { get; set; } //YAML/JSON/TOML
    }
}