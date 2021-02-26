using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class SourceControlRepository
    {
        public virtual string Name { get; set; }

        public virtual Uri Uri { get; set; }


        public SourceControlRepository()
        {
            Name = string.Empty;            
        }
    }
}
