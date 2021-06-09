using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedSolution : ILaunchPadGeneratedObject
    {
        public CloudInfrastructure Infrastructure { get; set; }

        /// <summary>
        /// Contains configuration information related to this solution
        /// </summary>
        public LaunchPadGeneratedSolutionSettings Settings { get; set; }


    }
}