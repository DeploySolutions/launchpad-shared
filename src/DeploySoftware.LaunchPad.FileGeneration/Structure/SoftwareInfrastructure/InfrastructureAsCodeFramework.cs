using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class InfrastructureAsCodeFramework : IInfrastructureAsCodeFramework
    {
        public virtual string Name { get; set; }

        public virtual string Version { get; set; }

        public virtual InfrastructureAsCodeFrameworkTypeEnum Type { get; set; } = InfrastructureAsCodeFrameworkTypeEnum.Terraform;

        public InfrastructureAsCodeFramework()
        {

        }
    }
}
