using System;

namespace Deploy.LaunchPad.Util
{
    public partial interface IElementDescription : IElementDescriptionLight, 
        IComparable<ElementDescriptionLight>, IEquatable<ElementDescriptionLight>,
        ICloneable, IAmCloneable<ElementDescription>
    {
        public string Short { get; set; }
    }
}