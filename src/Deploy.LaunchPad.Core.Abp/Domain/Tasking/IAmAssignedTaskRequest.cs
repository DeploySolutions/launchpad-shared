// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IAmAssignedTaskRequest.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Abp.Domain.Tasking
{
    /// <summary>
    /// Once a task is assigned to an assignee
    /// </summary>
    public partial interface IAmAssignedTaskRequest : IAmAssignableTask
    {
        /// <summary>
        /// Gets or sets the assignee.
        /// </summary>
        /// <value>The assignee.</value>
        public string Assignee { get; set; }
        /// <summary>
        /// Gets or sets the contributors.
        /// </summary>
        /// <value>The contributors.</value>
        public string Contributors { get; set; }
        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>The due date.</value>
        public DateTime? DueDate { get; set; }
    }
}
