using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.Util.Metadata
{
    
    public partial interface IMayHaveElementName
    {
        /// <summary>
        /// Name for this entity.
        /// </summary>
        public ElementName? Name { get; set; }

    }
}
