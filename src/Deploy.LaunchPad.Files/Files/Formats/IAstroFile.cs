
using Deploy.LaunchPad.Files.Formats;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Files
{
    public partial interface IAstroFile : IFile<string, AstroFileSchema>
    {
        public string? RawFrontmatter { get; set; }  // Entire JS/TS block

        public Dictionary<string, string>? Exports { get; set; }

        public IReadOnlyList<string> Imports { get; set; }

    }
}