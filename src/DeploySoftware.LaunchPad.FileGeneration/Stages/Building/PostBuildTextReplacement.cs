using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Stages
{
    [Serializable]
    public partial class PostBuildTextReplacement
    {
        public virtual string OriginalValue { get; set; } = "";

        public virtual string ReplacementValue { get; set; } = "";

        public PostBuildTextReplacement()
        {

        }
    }
}
