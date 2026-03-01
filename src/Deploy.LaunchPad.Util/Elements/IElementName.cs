using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Util.Elements
{
    public partial interface IElementName : IMustHaveFullName, 
        IMustHaveShortName,
        IComparable<ElementName>, 
        IEquatable<ElementName>,
        ICloneable, IAmCloneable<ElementName>
    {
        public string ShortName { get; set; }
    }
}