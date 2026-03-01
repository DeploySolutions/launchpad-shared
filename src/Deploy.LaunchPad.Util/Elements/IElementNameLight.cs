using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Util.Elements
{
    public partial interface IElementNameLight : IMustHaveFullName, IComparable<ElementNameLight>, 
        IEquatable<ElementNameLight>,
        ICloneable, IAmCloneable<ElementNameLight>
    {
    }
}