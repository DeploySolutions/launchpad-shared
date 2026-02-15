using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Core.Elements
{
    public partial interface IElementNameLight : IComparable<ElementNameLight>, 
        IEquatable<ElementNameLight>,
        ICloneable, IAmCloneable<ElementNameLight>
    {
        public string Full { get; set; }
    }
}