using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class LaunchPadGeneratedModuleBase : LaunchPadGeneratedObjectBase, ILaunchPadGeneratedModule
    {

        /// <summary>
        /// The version of this module
        /// </summary>
        public string Version { get; set; }
        public LaunchPadGeneratedModuleBase() : base()
        {
            Version = string.Empty;
        }
    }
}
