using Deploy.LaunchPad.Core.Files.Formats;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class OfficeWordDocument : FileBase<string, OfficeWordDocumentFileSchema>, IOfficeWordDocument
    {
        public override string Extension => "." + FileExtensions.docx;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeWordFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public OfficeWordDocument(string fileName) : base(fileName)
        {

        }
    }
}
