using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Metadata;
using System;

namespace Deploy.LaunchPad.Util.Elements
{
    public partial interface IElementName : IMustHaveFullName, 
        IMayHaveAlternateNames,
        IComparable<ElementName>, 
        IEquatable<ElementName>,
        ICloneable, IAmCloneable<ElementName>
    {
    }
}