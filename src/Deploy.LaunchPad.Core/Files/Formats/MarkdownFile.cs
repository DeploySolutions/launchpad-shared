﻿using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class MarkdownFile : FileBase<string>, IMarkdownFile
    {

        public override string Extension => ".md";

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownFile{TIdType}"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public MarkdownFile(string fileName) : base(fileName)
        {

        }
    }
}
