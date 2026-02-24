using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.Core.Metadata
{
    
    public partial interface IMayHaveName
    {
        /// <summary>
        /// Name for this entity.
        /// </summary>
        public ElementName? Name { get; set; }

    }
}
