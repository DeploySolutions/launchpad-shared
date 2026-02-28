using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Util.Elements
{
    public partial interface IElementName : IElementNameLight, IComparable<ElementName>, 
        IEquatable<ElementName>,
        ICloneable, IAmCloneable<ElementName>
    {
        public string Short { get; set; }
    }
}