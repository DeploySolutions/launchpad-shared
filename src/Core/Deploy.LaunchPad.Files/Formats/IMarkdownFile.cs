namespace Deploy.LaunchPad.Files.Formats
{
    public partial interface IMarkdownFile : IFile<string, MarkdownFileSchema>
    {
        public string Frontmatter { get; set; } //YAML/JSON/TOML
    }
}