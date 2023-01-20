using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    [Serializable]
    public partial class Release : IRelease
    {
        public virtual string Version { get; set; }

        public virtual string Checksum { get; set; }

        public virtual ReleaseSourceCode SourceCodeZip { get; set; }

        public virtual IDictionary<string, IReleaseAsset> Assets { get; set; }
        
        public virtual DateTime ReleaseDate { get; set; }

        public Release()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Assets = new Dictionary<string, IReleaseAsset>(comparer);
        }
    }
}
