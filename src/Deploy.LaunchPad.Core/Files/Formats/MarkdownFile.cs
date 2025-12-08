using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class MarkdownFile : FileBase<string, MarkdownFileSchema>, IMarkdownFile
    {
        public virtual string Frontmatter { get; set; } //YAML/JSON/TOML
        public override string Extension => "." + FileExtensions.md;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public MarkdownFile(string fileName) : base(fileName)
        {

        }
    }
}
