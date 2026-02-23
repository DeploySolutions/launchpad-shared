using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Util.Elements
{
    public partial interface IElementNameLight : IComparable<ElementNameLight>, 
        IEquatable<ElementNameLight>,
        ICloneable, IAmCloneable<ElementNameLight>
    {
        public string Full { get; set; }
    }
}