using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Deploy.LaunchPad.Core.Metadata
{
    public partial interface IMayHaveLastModifierUserId
    {
        /// <summary>
        /// Last modifier user for this entity.
        /// </summary>
        [Column("core_user_last_modifier_id")]
        System.Guid? LastModifierUserId { get; set; }
    }
}
