using Deploy.LaunchPad.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Abp.Domain.Tasking
{
    public partial interface IAmAssignableTask : ILaunchPadCommonProperties
    {        

        public string InstructionsShort { get; set; }
        public string InstructionsDetailed { get; set; }

        public Uri MoreInformationUri { get; set; }

        public IDictionary<string, IAmAssignableTask> Dependencies { get; set; }

        public IDictionary<string, IAmAssignableTask> SubTasks { get; set; }

        public string Priority { get; set; }
    }
}
