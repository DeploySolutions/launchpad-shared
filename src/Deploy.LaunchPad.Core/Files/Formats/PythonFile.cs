using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class PythonFile : FileBase<string, PythonSchema>, IPythonFile
    {

        public override string Extension => ".py";

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public PythonFile(string fileName) : base(fileName)
        {

        }
    }
}
