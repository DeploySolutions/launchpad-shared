using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Deploy.LaunchPad.Util.Metadata
{
    public partial interface IMayHaveDeleterUserId
    {
        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        [Column("core_user_deleter_id")]
        Guid? DeleterUserId { get; set; }
    }
}
