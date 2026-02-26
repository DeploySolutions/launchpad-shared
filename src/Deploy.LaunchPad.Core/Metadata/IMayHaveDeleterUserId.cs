using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial interface IMayHaveDeleterUserId
    {
        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        long? DeleterUserId { get; set; }
    }
}
