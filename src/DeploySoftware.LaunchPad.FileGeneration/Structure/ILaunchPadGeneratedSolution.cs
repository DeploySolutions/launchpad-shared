using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public interface ILaunchPadGeneratedSolution : ILaunchPadGeneratedObject
    {

        /// <summary>
        /// Contains configuration information related to this solution
        /// </summary>
        public LaunchPadGeneratedSolutionSettings Settings { get; set; }


    }
}