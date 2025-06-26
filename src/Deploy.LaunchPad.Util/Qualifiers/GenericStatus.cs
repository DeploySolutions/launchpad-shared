using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.Qualifiers
{
    public enum GenericStatus
    {

        [Description("cancelled")]
        Cancelled = 5,

        [Description("completed")]
        Completed = 4,

        [Description("inactive")]
        Inactive = 3,

        [Description("active")]
        Active = 2,

        [Description("pending")]
        Pending = 1,

        [Description("unknown")]
        Unknown = 0,

    }
}
