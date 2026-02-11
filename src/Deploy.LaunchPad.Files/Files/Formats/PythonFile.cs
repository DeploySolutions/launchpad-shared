
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Files.Formats;

using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Files
{
    public partial class PythonFile : FileBase<string, PythonFileSchema>, IPythonFile
    {
        public override string Extension => "." + FileExtensions.py;

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public PythonFile(string fileName) : base(fileName)
        {

        }
    }
}
