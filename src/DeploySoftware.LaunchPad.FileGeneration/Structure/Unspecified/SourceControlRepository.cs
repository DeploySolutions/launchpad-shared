﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
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