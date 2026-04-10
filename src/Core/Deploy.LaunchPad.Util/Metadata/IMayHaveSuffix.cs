using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Util.Metadata
{
    public partial interface IMayHaveSuffix
    {
        string? Suffix { get; set; }
    }
}
