using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represent a C# method parameter or field
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadGeneratedMethodFieldBase : LaunchPadGeneratedObjectBase
    {

        public virtual LaunchPadGeneratedItemType ItemType { get; set; }

        public LaunchPadGeneratedMethodFieldBase() : base()
        {
            ItemType = LaunchPadGeneratedItemType.Custom;
        }

    }
}
