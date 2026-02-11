
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Files.Formats;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Files
{
    public partial class JupyterNotebookFile : FileBase<byte[], JToken>, IJupyterNotebookFile
    {
        public override string Extension => "." + FileExtensions.ipynb;

        /// <summary>
        /// Constructor
        /// </summary>
        public JupyterNotebookFile(string fileName) : base(fileName)
        {
        }
    }
}
