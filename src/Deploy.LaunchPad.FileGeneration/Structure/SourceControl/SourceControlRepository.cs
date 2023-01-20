using System;
using System.Collections;

namespace Deploy.LaunchPad.FileGeneration.Structure.SourceControl
{
    [Serializable]
    public partial class SourceControlRepository : ISourceControlRepository
    {
        public virtual string Name { get; set; }

        public virtual Uri Uri { get; set; }



        public SourceControlRepository()
        {
            Name = string.Empty;
        }
    }
}
