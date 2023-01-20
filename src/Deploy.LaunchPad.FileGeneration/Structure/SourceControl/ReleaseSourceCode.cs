using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    [Serializable]
    public partial class ReleaseSourceCode : ReleaseAssetBase
    {

        public virtual Uri SourceCodeTarDownload { get; set; }

        public virtual Uri SourceCodeZipDownload { get; set; }

        public ReleaseSourceCode() { }
    }
}
