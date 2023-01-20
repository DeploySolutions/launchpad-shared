using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    [Serializable]
    public partial class GitRepository : SourceControlRepository
    {


        public virtual string DefaultBranch { get; set; }

        public virtual IDictionary<string, Uri> Branches { get; set; }


        public GitRepository() :base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Branches = new Dictionary<string, Uri>(comparer);
        }
    }
}
