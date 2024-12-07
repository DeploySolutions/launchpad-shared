﻿using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class PdfFile : FileBase<byte[]>, IPdfFile
    {

        public override string Extension => ".pdf";


        /// <summary>
        /// Constructor
        /// </summary>
        public PdfFile(string fileName) : base(fileName)
        {
        }
    }
}