﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
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
