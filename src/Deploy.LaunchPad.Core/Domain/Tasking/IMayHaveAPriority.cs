using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Tasking
{
    public partial interface IMayHaveAPriority
    {
        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public PriorityLevel? Priority { get; set; }
    }
}
