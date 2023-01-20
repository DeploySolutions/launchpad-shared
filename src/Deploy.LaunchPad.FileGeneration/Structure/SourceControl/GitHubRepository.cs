using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    [Serializable]
    public partial class GitHubRepository : GitRepository
    {

        public virtual string Org { get; set; } = string.Empty;


        public virtual IDictionary<string, IRelease> Releases { get; set; }

        public GitHubRepository() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Releases = new Dictionary<string, IRelease>(comparer);
        }
    }
}
