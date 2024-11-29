using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class MarkdownFile : FileBase<string>, IMarkdownFile
    {

        public override string Extension => ".md";

        public MarkdownFile()
        {
        }
    }
}
