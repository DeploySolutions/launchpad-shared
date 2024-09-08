using System;

namespace Deploy.LaunchPad.Core
{
    public partial interface IElementDescription : IComparable<ElementDescription>, IEquatable<ElementDescription>
    {
        public string Full { get; set; }
        public string Short { get; set; }
    }
}