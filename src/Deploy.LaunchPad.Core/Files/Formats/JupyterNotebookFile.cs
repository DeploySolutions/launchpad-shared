using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class JupyterNotebookFile : FileBase<byte[]>, IJupyterNotebookFile
    {

        public override string Extension => ".ipynb";

        public JupyterNotebookFile()
        {
        }
    }
}
