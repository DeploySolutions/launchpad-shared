// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Code.Secrets
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-19-2023
// ***********************************************************************
// <copyright file="ILaunchPadSecretConfiguration.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.Secrets
{
    /// <summary>
    /// Interface ILaunchPadSecretFields
    /// </summary>
    public partial interface ILaunchPadSecretFields
    {
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public IDictionary<string, string> Fields { get; set; }

        public bool AddField(string key, string value);

        public bool UpdateField(string key, string value);

        public bool RemoveField(string key, string value);

        public string GetValue(string key, string caller, bool keyIsCaseInsensitive = true);

        /// <summary>
        /// Finds the values for keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
        public IDictionary<string, string> FindValuesForKeys(IList<string> keys, string caller, bool keyIsCaseInsensitive = true);

    }
}
