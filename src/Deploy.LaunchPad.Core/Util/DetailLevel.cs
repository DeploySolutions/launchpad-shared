using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Util
{
    /// <summary>
    /// Useful for specifying the level of detail required for a method's return
    /// </summary>
    public enum DetailLevel
    {
        Basic = 0,
        Detailed = 1,
        Full = 2,
        Admin = 3
    }
}
