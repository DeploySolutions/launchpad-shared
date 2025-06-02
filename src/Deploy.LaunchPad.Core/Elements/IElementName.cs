using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Core
{
    public partial interface IElementName : IElementNameLight, IComparable<ElementName>, 
        IEquatable<ElementName>,
        ICloneable, IAmCloneable<ElementName>
    {
        public string Prefix { get; set; }
        public string Short { get; set; }
        public string Suffix { get; set; }
    }
}