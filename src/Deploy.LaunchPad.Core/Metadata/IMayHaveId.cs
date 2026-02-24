
namespace Deploy.LaunchPad.Core.Metadata
{
    
    public partial interface IMayHaveId<TPrimaryKey>
    {
        /// <summary>
        /// Name for this entity.
        /// </summary>
        public TPrimaryKey? Id { get; set; }

    }
}
