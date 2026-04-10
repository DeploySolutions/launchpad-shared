using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Metadata;
using System;

namespace Deploy.LaunchPad.Util.Elements
{
    public partial interface IElementDescriptionLight : IMustHaveFullDescription, 
        IComparable<ElementDescriptionLight>, 
        IEquatable<ElementDescriptionLight>,
        ICloneable, IAmCloneable<ElementDescriptionLight>
    {
    }
}