using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Core.Elements
{
    public partial interface IElementDescription : IElementDescriptionLight, 
        IComparable<ElementDescriptionLight>, IEquatable<ElementDescriptionLight>,
        ICloneable, IAmCloneable<ElementDescription>
    {
        public string Short { get; set; }
    }
}