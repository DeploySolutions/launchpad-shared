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
        public string Name { get; set; }

        public Uri Uri { get; set; }


        public SourceControlRepository()
        {
            Name = string.Empty;            
        }
    }
}
