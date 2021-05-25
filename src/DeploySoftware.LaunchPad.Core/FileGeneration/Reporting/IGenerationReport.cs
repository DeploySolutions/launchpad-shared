using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface IGenerationReport<TSolution>
        where TSolution: LaunchPadGeneratedSolution, new()
    {

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
        /// Convenience method to determine the time between start and end of duration.
        /// </summary>
        /// <returns>The TimeSpan between the start and end, or null if generation has not yet ended.</returns>
        public TimeSpan? GetGenerationTime();

        public TSolution GeneratedSolution { get; set; }

        public IDictionary<string, Exception> Exceptions { get; set; }

        public IDictionary<string, string> Warnings { get; set; }

        public IDictionary<string, string> Infos { get; set; }

    }
}
