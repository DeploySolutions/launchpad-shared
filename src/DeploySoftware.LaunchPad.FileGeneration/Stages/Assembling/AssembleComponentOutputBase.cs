
using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.FileGeneration.Stages
{
    public abstract partial class AssembleComponentOutputBase : LaunchPadGenerationOutputBase
    {

        public DateTime AssemblyStarted { get; set; }

        public DateTime AssemblyEnded { get; set; }

        public TimeSpan AssemblyDuration { get; set; }

        public string AssemblyOutputMessage { get; set; }

        public int FileCount { get; set; }


    }
}
