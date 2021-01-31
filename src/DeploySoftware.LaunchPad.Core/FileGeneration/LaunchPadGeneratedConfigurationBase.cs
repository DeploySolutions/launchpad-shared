using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class LaunchPadGeneratedConfigurationBase
    {

        /// <summary>
        /// The name of the Solution in which this generated module will belong.
        /// </summary>
        public string SolutionName { get; set; }

        public SourceControlRepository Repository { get; set; }


        public LaunchPadGeneratedConfigurationBase()
        {
            SolutionName = string.Empty;
            Repository = new SourceControlRepository();
        }

    }
}
