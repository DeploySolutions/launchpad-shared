using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class SourceControlRepository
    {
        public virtual string Name { get; set; }

        public virtual Uri Uri { get; set; }
        public virtual string Org { get; set; }


        public SourceControlRepository()
        {
            Name = string.Empty;
            Org = string.Empty;
        }
    }
}
