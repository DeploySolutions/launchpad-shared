
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class AssembleModuleInputBase<TGeneratedSolution> : AssembleInputBase
        where TGeneratedSolution : LaunchPadGeneratedSolution, new()
    {

        public TGeneratedSolution GeneratedSolutionDefinition { get; set; }        

    }
}
