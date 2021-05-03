
using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class AssembleOutputBase<TGeneratedObject>
        where TGeneratedObject : LaunchPadGeneratedObjectBase, new()
    {

        public DateTime AssemblyStarted { get; set; }

        public DateTime AssemblyEnded { get; set; }

        public TimeSpan AssemblyDuration { get; set; }

        public bool Succeeded { get; set; }

        public string AssemblyOutputMessage { get; set; }

        public int FileCount { get; set; }


    }
}
