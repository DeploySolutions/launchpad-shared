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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.Secrets
{
    /// <summary>
    /// Class LaunchPadAbpModuleSecretConfiguration.
    /// Implements the <see cref="Deploy.LaunchPad.Util.Secrets.ILaunchPadSecretFields" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Util.Secrets.ILaunchPadSecretFields" />
    public partial class LaunchPadSecretFields : ILaunchPadSecretFields
    {
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public required virtual IDictionary<string, string> Fields { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadSecretFields"/> class.
        /// </summary>
        [SetsRequiredMembers]
        public LaunchPadSecretFields()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);
        }

        public virtual bool AddField(string key, string value)
        {
            Guard.AgainstNullOrEmpty(key, nameof(key));
            Guard.AgainstNullOrEmpty(value, nameof(value));
            if (Fields.ContainsKey(key))
            {
                return false;
            }
            Fields.Add(key, value);
            return true;
        }

        public virtual bool UpdateField(string key, string value)
        {
            Guard.AgainstNullOrEmpty(key, nameof(key));
            Guard.AgainstNullOrEmpty(value, nameof(value));
            if (!Fields.ContainsKey(key))
            {
                return false;
            }
            Fields[key] = value;
            return true;
        }

        public virtual bool RemoveField(string key, string value)
        {
            Guard.AgainstNullOrEmpty(key, nameof(key));
            Guard.AgainstNullOrEmpty(value, nameof(value));
            if (!Fields.ContainsKey(key))
            {
                return false;
            }
            Fields.Remove(key);
            return true;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>System.String.</returns>
        public virtual string GetValue(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            string value = string.Empty;
            if (keyIsCaseInsensitive)
            {
                value = Fields.FirstOrDefault(k => k.Key.ToLower() == key.ToLower()).Value;
            }
            else
            {
                value = Fields.FirstOrDefault(k => k.Key == key).Value;
            }
            return value;
        }

        /// <summary>
        /// Finds the values for keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
        public virtual IDictionary<string, string> FindValuesForKeys(IList<string> keys, string caller, bool keyIsCaseInsensitive = true)
        {
            IDictionary<string, string> kvps = new Dictionary<string, string>();
            // loop through the desired set of keys to find the corresponding values in the JSON
            if (keyIsCaseInsensitive)
            {
                kvps = (IDictionary<string, string>)keys.Where(k => Fields.ContainsKey(k.ToLower()));
            }
            else
            {
                kvps = (IDictionary<string, string>)keys.Where(k => Fields.ContainsKey(k));
            }

            return kvps;
        }

    }
}
