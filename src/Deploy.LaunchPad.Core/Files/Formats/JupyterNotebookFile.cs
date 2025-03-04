﻿using Deploy.LaunchPad.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class JupyterNotebookFile : FileBase<byte[], JToken>, IJupyterNotebookFile
    {

        public override string Extension => ".ipynb";

        /// <summary>
        /// Constructor
        /// </summary>
        public JupyterNotebookFile(string fileName) : base(fileName)
        {
        }
    }
}
