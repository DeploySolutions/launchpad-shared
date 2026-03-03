// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Code.Secrets
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-19-2023
// ***********************************************************************
// <copyright file="LaunchPadSecretConfiguration.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Secrets
{
    /// <summary>
    /// Class LaunchPadAbpModuleSecretConfiguration.
    /// Implements the <see cref="Deploy.LaunchPad.Code.Secrets.ILaunchPadSecretConfiguration" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Code.Secrets.ILaunchPadSecretConfiguration" />
    public partial class LaunchPadSecretConfiguration : ILaunchPadSecretConfiguration
    {
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public virtual IDictionary<string, string> Fields { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadSecretConfiguration"/> class.
        /// </summary>
        public LaunchPadSecretConfiguration()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);
        }
    }
}
