
using System.ComponentModel.DataAnnotations.Schema;

namespace Deploy.LaunchPad.Core.Metadata
{
    
    public partial interface IMayHaveId<TPrimaryKey>
    {
        /// <summary>
        /// Name for this entity.
        /// </summary>
        [Column("core_id")]
        public TPrimaryKey? Id { get; set; }

    }
}
