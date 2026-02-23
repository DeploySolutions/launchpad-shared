
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;


namespace Deploy.LaunchPad.Files.Formats
{
    public partial class OfficeWordDocument : FileBase<string, OfficeWordDocumentFileSchema>, IOfficeWordDocument
    {
        public override string Extension => "." + FileExtension.docx; // could also use Libre Office extensions

        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeWordFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public OfficeWordDocument(string fileName) : base(fileName)
        {

        }
    }
}
