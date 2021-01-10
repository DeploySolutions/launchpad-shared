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

        /// <summary>
        /// Code annotations for the object
        /// </summary>
        public string Annotations { get; set; }

        public LaunchPadGeneratedMethodFieldBase():  base()
        {
            Annotations = string.Empty;
        }

    }
}
