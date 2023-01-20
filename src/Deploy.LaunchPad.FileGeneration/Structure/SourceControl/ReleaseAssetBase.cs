using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    [Serializable]
    public abstract partial class ReleaseAssetBase : IReleaseAsset
    {
        public virtual string Name { get; set; }

        public virtual Uri Uri { get; set; }

        public ReleaseAssetBase() { }
    }
}
