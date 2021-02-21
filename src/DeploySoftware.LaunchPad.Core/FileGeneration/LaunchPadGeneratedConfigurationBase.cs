using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class LaunchPadGeneratedConfigurationBase
    {

        public SourceControlRepository Repository { get; set; }


        public LaunchPadGeneratedConfigurationBase()
        {
            Repository = new SourceControlRepository();
        }

    }
}
