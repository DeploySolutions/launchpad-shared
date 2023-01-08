using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class LaunchPadGeneratedFolder : LaunchPadGeneratedObjectBase
    {
        public virtual IList<LaunchPadGeneratedFolder> SubFolders { get; set; }

        public LaunchPadGeneratedFolder() : base()
        {
            SubFolders = new List<LaunchPadGeneratedFolder>();
        }
    }
}
