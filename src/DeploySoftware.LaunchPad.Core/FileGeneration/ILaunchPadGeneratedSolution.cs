using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedSolution : ILaunchPadGeneratedObject
    {
        public CloudInfrastructure Infrastructure { get; set; }

        /// <summary>
        /// The date time the solution generation started
        /// </summary>
        public DateTime GenerationStart { get; set; }

        /// <summary>
        /// The date time the solution generation ended, or null if not yet complete
        /// </summary>
        public DateTime? GenerationEnd { get; set; }

        public TimeSpan? GenerationDuration { get; set; }

        /// <summary>
        /// Contains configuration information related to this solution
        /// </summary>
        public LaunchPadGeneratedSolutionSettings Settings { get; set; }

        /// <summary>
        /// Convenience method to determine the time between start and end of duration.
        /// </summary>
        /// <returns>The TimeSpan between the start and end, or null if generation has not yet ended.</returns>
        public TimeSpan? GetGenerationTime();

    }
}