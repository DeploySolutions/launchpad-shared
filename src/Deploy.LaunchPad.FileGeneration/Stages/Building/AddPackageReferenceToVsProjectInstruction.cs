using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Stages.Building
{
    [Serializable]
    public partial class AddPackageReferenceToVsProjectInstruction
    {
        public virtual string CsprojFilePath { get; set; }

        public virtual string Include { get; set; }

        public virtual string IncludeAfter { get; set; }

        public virtual string Version { get; set; }

        public AddPackageReferenceToVsProjectInstruction()
        {   

        }

    }
}
