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
using Deploy.LaunchPad.Util.Metadata;

namespace Deploy.LaunchPad.Domain.Tasking
{
    /// <summary>
    /// Interface IAmAssignableTask
    /// Extends the <see cref="ILaunchPadCommonProperties" />
    /// </summary>
    /// <seealso cref="ILaunchPadCommonProperties" />
    public partial interface IAmAssignableTask : ILaunchPadCommonProperties, IMayHaveAPriority
    {
        public TaskInstruction Instructions { get; set; }

        /// <summary>
        /// Gets or sets the dependent on.
        /// </summary>
        /// <value>The dependent on.</value>
        public IList<IAmAssignableTask>? DependentOn { get; set; }

        /// <summary>
        /// Gets or sets the sub tasks.
        /// </summary>
        /// <value>The sub tasks.</value>
        public IList<IAmAssignableTask>? SubTasks { get; set; }

    }
}
