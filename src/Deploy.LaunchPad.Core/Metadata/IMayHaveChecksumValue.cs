using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.Core.Metadata
{
    
    public partial interface IMayHaveChecksumValue
    {
        /// <summary>
        /// Checksum value for this entity. Implementers decide
        /// </summary>
        public string? Checksum { get; set; }

        public string ComputeChecksum(string input = "");
    }
}
