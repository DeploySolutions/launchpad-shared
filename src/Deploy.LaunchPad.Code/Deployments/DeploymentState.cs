using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Code.Deployments
{
    /// <summary>
    /// The possible states in which this deployment can be
    /// </summary>
    public enum DeploymentState
    {
        /// <summary>
        /// The not started
        /// </summary>
        Not_Started = 0,
        /// <summary>
        /// The in progress
        /// </summary>
        In_Progress = 1,
        /// <summary>
        /// The succeeded
        /// </summary>
        Succeeded = 2,
        /// <summary>
        /// The failed
        /// </summary>
        Failed = 3
    }


}
