
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Files.Formats;

using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Files
{
    public partial class PdfFile : FileBase<byte[], PdfFileSchema>, IPdfFile
    {
        public override string Extension => "." + FileExtensions.pdf;


        /// <summary>
        /// Constructor
        /// </summary>
        public PdfFile(string fileName) : base(fileName)
        {
        }
    }
}
