using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class PdfFile : FileBase<byte[], PdfSchemaFormat>, IPdfFile
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
