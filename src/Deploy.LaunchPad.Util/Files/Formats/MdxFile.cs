using Deploy.LaunchPad.Util.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Util.Files
{
    /// <summary>
    /// MDX (Markdown for Components)
    /// </summary>
    public partial class MdxFile : FileBase<string, MdxFileSchema>, IMdxFile
    {
        public override string Extension => "." + FileExtensions.mdx;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdxFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public MdxFile(string fileName) : base(fileName)
        {

        }
    }
}
