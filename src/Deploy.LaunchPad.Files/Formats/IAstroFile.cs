using System.Collections.Generic;

namespace Deploy.LaunchPad.Files.Formats
{
    public partial interface IAstroFile : IFile<string, AstroFileSchema>
    {
        public string? RawFrontmatter { get; set; }  // Entire JS/TS block

        public Dictionary<string, string>? Exports { get; set; }

        public IReadOnlyList<string> Imports { get; set; }

    }
}