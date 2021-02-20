using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represent a C# method parameter or field
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadGeneratedMethodFieldBase: LaunchPadGeneratedObjectBase
    {

        public LaunchPadGeneratedItemType ItemType { get; set; }

        public LaunchPadGeneratedMethodFieldBase():  base()
        {
            ItemType = LaunchPadGeneratedItemType.Custom;
        }

    }
}
