using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial interface IMayHaveLastModifierUserId
    {
        /// <summary>
        /// Last modifier user for this entity.
        /// </summary>
        long? LastModifierUserId { get; set; }
    }
}
