using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class CSharpClassFile : FileBase<string, CSharpClassSchema>, ICSharpClassFile
    {

        public override string Extension => ".cs";

        /// <summary>
        /// Initializes a new instance of the <see cref="CSharpClassFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public CSharpClassFile(string fileName) : base(fileName)
        {

        }
    }
}
