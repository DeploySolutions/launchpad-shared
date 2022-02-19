using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Stages
{
    public abstract partial class LaunchPadGenerationOutputBase : LaunchPadGenerationInputBase, ILaunchPadGenerationOutput
    {
        public virtual bool Succeeded { get; set; }
    }
}
