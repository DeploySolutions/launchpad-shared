using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class AstroFile : FileBase<string, AstroFileSchema>, IAstroFile
    {
        public virtual string? RawFrontmatter { get; set; }  // Entire JS/TS block

        public virtual Dictionary<string, string>? Exports { get; set; }
            = new(); // e.g. title, description

        public virtual IReadOnlyList<string> Imports { get; set; }
            = Array.Empty<string>(); // import Footer... etc.

        public override string Extension => "." + FileExtensions.astro;

        /// <summary>
        /// Initializes a new instance of the <see cref="AstroFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public AstroFile(string fileName) : base(fileName)
        {

        }
    }
}
