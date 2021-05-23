using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedSolution : ILaunchPadGeneratedObject
    {
        /// <summary>
        /// The date time the solution generation started
        /// </summary>
        public DateTime GenerationStart { get; set; }

        /// <summary>
        /// The date time the solution generation ended, or null if not yet complete
        /// </summary>
        public DateTime? GenerationEnd { get; set; }

        /// <summary>
        /// Contains configuration information related to this solution
        /// </summary>
        public LaunchPadGeneratedSolutionSettings Settings { get; set; }

    }
}