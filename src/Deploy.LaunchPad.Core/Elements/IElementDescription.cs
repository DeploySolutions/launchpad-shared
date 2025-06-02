using Deploy.LaunchPad.Util;
using Schema.NET;
using System;

namespace Deploy.LaunchPad.Core
{
    public partial interface IElementDescription : IElementDescriptionLight, 
        IComparable<ElementDescriptionLight>, IEquatable<ElementDescriptionLight>,
        ICloneable, IAmCloneable<ElementDescription>
    {
        public string Short { get; set; }
    }
}