using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Tasking
{
    /// <summary>
    /// Once a task is assigned to an assignee
    /// </summary>
    public partial interface IAmAssignedTaskRequest<TTdType> : IAmAssignableTask<TTdType>
    {
        public string Assignee { get; set; }
        public string Contributors { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
