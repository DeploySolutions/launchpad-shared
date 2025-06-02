using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Core
{
    public partial interface IElementDescriptionLight : IComparable<ElementDescriptionLight>, 
        IEquatable<ElementDescriptionLight>,
        ICloneable, IAmCloneable<ElementDescriptionLight>
    {
        public string Full { get; set; }
    }
}