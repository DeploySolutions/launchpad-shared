// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IAmAssignedTaskResponse.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <typeparam name="TTdType">The type of the t td type.</typeparam>
    public partial interface IAmAssignedTaskResponse<TTdType> : IAmAssignedTaskRequest<TTdType>
    {
        /// <summary>
        /// Gets or sets the progress.
        /// </summary>
        /// <value>The progress.</value>
        public string Progress { get; set; }

        /// <summary>
        /// Gets or sets the completed date.
        /// </summary>
        /// <value>The completed date.</value>
        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }
    }
}
