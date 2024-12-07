using System;

namespace Deploy.LaunchPad.Core
{
    public partial interface IElementName : IComparable<ElementName>, IEquatable<ElementName>
    {
        public string Full { get; set; }
        public string Prefix { get; set; }
        public string Short { get; set; }
        public string Suffix { get; set; }
    }
}