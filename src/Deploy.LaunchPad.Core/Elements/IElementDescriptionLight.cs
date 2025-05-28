using System;

namespace Deploy.LaunchPad.Core
{
    public partial interface IElementDescriptionLight : IComparable<ElementDescriptionLight>, IEquatable<ElementDescriptionLight>
    {
        public string Full { get; set; }
    }
}