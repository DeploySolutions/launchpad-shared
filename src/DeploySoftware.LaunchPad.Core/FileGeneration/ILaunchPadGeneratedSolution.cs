using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedSolution : ILaunchPadGeneratedObject
    {
        /// <summary>
        /// Contains configuration information related to this object's solution (.sln)
        /// </summary>
        public ILaunchPadGeneratedSolutionConfiguration Config { get; set; }

        /// <summary>
        /// The set of components that belong to this module.
        /// </summary>
        public IDictionary<string, ILaunchPadGeneratedModule> Modules { get; set; }

    }
}