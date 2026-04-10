using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Util.Metadata
{
    public partial interface IMayHavePrefix
    {
        string? Prefix { get; set; }
    }
}
