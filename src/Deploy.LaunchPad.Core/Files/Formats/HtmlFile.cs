using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Files
{
    public partial class HtmlFile : FileBase<string>, IHtmlFile
    {

        public override string Extension => ".html";

        public HtmlFile()
        {
        }
    }
}
