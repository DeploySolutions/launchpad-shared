using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Util.Metadata
{
    public partial interface IMayHaveAlternateNames : IMustHaveFullName, IMustHaveShortName
    {
        string? Suffix { get; }
        
        string? Prefix { get; }

        IDictionary<string, string> AlternateNames { get; }
    }
}
