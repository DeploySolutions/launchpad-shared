using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.Core.Metadata
{
    
    public partial interface IMayHaveDescription
    {
        /// <summary>
        /// Description for this entity.
        /// </summary>
        public ElementDescription? Description { get; set; }

    }
}
