
using Deploy.LaunchPad.Util;

using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Files.Formats
{
    /// <summary>
    /// Toon (Token-Oriented Object Notation)
    /// </summary>
    public partial class ToonFile : FileBase<string, ToonFileSchema>, IToonFile
    {
        public override string Extension => "." + FileExtension.toon;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToonFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public ToonFile(string fileName) : base(fileName)
        {

        }
    }
}
