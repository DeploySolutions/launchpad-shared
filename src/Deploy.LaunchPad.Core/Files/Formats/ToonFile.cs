using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    /// <summary>
    /// Toon (Token-Oriented Object Notation)
    /// </summary>
    public partial class ToonFile : FileBase<string, ToonFileSchema>, IToonFile
    {
        public override string Extension => "." + FileExtensions.toon;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToonFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public ToonFile(string fileName) : base(fileName)
        {

        }
    }
}
