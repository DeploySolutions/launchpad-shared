using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Deploy.LaunchPad.Util.Metadata
{
    public partial interface IMayHaveCreatorUserId
    {
        /// <summary>
        /// Id of the creator user of this entity.
        /// </summary>
        [Column("core_user_creator_id")]
        System.Guid? CreatorUserId { get; set; }
    }
}
