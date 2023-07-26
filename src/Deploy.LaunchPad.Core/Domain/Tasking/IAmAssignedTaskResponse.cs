using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Tasking
{
    /// <summary>
    /// Once an assigned task is responded to by the assignee
    /// </summary>
    public partial interface IAmAssignedTaskResponse<TTdType> : IAmAssignedTaskRequest<TTdType>
    {
        public string Progress { get; set; }

        public DateTime? CompletedDate { get; set; }

        public string Comments { get; set; }
    }
}
