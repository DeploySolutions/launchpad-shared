using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class GenerationReport<TSolution> : IGenerationReport<TSolution>
        where TSolution : LaunchPadGeneratedSolution, new()
    {
        /// <summary>
        /// The date time the solution generation started
        /// </summary>
        public virtual DateTime GenerationStart { get; set; }

        /// <summary>
        /// The date time the solution generation ended, or null if not yet complete
        /// </summary>
        public virtual DateTime? GenerationEnd { get; set; }

        public TimeSpan? GenerationDuration { get; set; }

        public virtual TSolution GeneratedSolution { get; set; }

        public virtual IDictionary<string, Exception> Exceptions { get; set; }

        public virtual IDictionary<string,string> Warnings { get; set; }

        public virtual IDictionary<string,string> Infos { get; set; }


        public virtual TimeSpan? GetGenerationTime()
        {
            if (GenerationEnd.HasValue)
            {
                TimeSpan duration = GenerationEnd.Value - GenerationStart;
                return duration;
            }
            else
            {
                return null;
            }
        }

        public GenerationReport()
        {
            GenerationStart = DateTime.UtcNow;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Exceptions = new Dictionary<string, Exception>(comparer);
            Warnings = new Dictionary<string, string>(comparer);
            Infos = new Dictionary<string, string>(comparer);
        }

        public GenerationReport(DateTime start)
        {
            GenerationStart = start;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Exceptions = new Dictionary<string, Exception>(comparer);
            Warnings = new Dictionary<string, string>(comparer);
            Infos = new Dictionary<string, string>(comparer);
        }

    }
}
