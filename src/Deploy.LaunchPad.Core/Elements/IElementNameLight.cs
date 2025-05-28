using System;

namespace Deploy.LaunchPad.Core
{
    public partial interface IElementNameLight : IComparable<ElementNameLight>, IEquatable<ElementNameLight>
    {
        public string Full { get; set; }
    }
}