using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public partial class LaunchPadGeneratedFolder : LaunchPadGeneratedObjectBase
    {
        public IList<LaunchPadGeneratedFolder> SubFolders { get; set; }

        public LaunchPadGeneratedFolder() : base()
        {
            SubFolders = new List<LaunchPadGeneratedFolder>();
        }
    }
}
