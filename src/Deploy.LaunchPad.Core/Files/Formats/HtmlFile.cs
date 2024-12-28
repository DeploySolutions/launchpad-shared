﻿using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class HtmlFile : FileBase<string, XmlSchemaSet>, IHtmlFile
    {

        public override string Extension => ".html";

        /// Initializes a new instance of the <see cref="HtmlFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public HtmlFile(string fileName) : base(fileName)
        {

        }
    }
}
