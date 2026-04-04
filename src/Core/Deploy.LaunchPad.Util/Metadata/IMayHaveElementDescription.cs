using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.Util.Metadata
{
    
    public partial interface IMayHaveElementDescription
    {
        /// <summary>
        /// Description for this entity.
        /// </summary>
        public ElementDescription? Description { get; set; }

    }
}
