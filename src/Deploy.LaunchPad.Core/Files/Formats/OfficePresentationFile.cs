using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class OfficePresentationFile : FileBase<byte[], OfficePresentationFileSchema>, IOfficePresentationFile
    {
        public override string Extension => "." + FileExtensions.pptx;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficePresentationFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public OfficePresentationFile(string fileName) : base(fileName)
        {

        }
    }
}
