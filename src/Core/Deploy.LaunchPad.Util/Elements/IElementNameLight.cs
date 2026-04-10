using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Metadata;
using System;

namespace Deploy.LaunchPad.Util.Elements
{
    public partial interface IElementNameLight : IMustHaveFullName, IComparable<ElementNameLight>, 
        IEquatable<ElementNameLight>,
        ICloneable, IAmCloneable<ElementNameLight>
    {
    }
}