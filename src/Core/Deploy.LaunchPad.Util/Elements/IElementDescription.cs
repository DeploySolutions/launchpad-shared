using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Metadata;
using System;

namespace Deploy.LaunchPad.Util.Elements
{
    public partial interface IElementDescription : IElementDescriptionLight, IMustHaveShortDescription,
        IComparable<ElementDescriptionLight>, IEquatable<ElementDescriptionLight>,
        ICloneable, IAmCloneable<ElementDescription>
    {
    }
}