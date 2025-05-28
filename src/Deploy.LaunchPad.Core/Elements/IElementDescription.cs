using System;

namespace Deploy.LaunchPad.Core
{
    public partial interface IElementDescription : IElementDescriptionLight, IComparable<ElementDescriptionLight>, IEquatable<ElementDescriptionLight>
    {
        public string Short { get; set; }
    }
}