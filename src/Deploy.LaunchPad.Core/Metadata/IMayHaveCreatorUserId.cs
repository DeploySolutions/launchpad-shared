using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial interface IMayHaveCreatorUserId
    {
        /// <summary>
        /// Id of the creator user of this entity.
        /// </summary>
        long? CreatorUserId { get; set; }
    }
}
