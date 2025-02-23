// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IAmAssignableTask.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Tasking
{
    /// <summary>
    /// Interface IAmAssignableTask
    /// Extends the <see cref="ILaunchPadCommonProperties" />
    /// </summary>
    /// <seealso cref="ILaunchPadCommonProperties" />
    public partial interface IAmAssignableTask : ILaunchPadCommonProperties
    {

        /// <summary>
        /// Gets or sets the instructions short.
        /// </summary>
        /// <value>The instructions short.</value>
        public string InstructionsShort { get; set; }
        /// <summary>
        /// Gets or sets the instructions detailed.
        /// </summary>
        /// <value>The instructions detailed.</value>
        public string InstructionsDetailed { get; set; }

        /// <summary>
        /// Gets or sets the more information URI.
        /// </summary>
        /// <value>The more information URI.</value>
        public Uri MoreInformationUri { get; set; }

        /// <summary>
        /// Gets or sets the dependent on.
        /// </summary>
        /// <value>The dependent on.</value>
        public IDictionary<string, IAmAssignableTask> DependentOn { get; set; }

        /// <summary>
        /// Gets or sets the sub tasks.
        /// </summary>
        /// <value>The sub tasks.</value>
        public IDictionary<string, IAmAssignableTask> SubTasks { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public PriorityLevel Priority { get; set; }
    }
}
