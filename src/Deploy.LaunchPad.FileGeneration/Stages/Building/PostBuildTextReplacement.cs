using System;

namespace Deploy.LaunchPad.FileGeneration.Stages
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
