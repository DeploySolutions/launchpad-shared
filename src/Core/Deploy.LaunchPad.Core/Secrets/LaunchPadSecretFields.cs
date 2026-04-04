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
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets
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
        public required virtual IDictionary<string, ISettingDefinition> Fields { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadSecretFields"/> class.
        /// </summary>
        [SetsRequiredMembers]
        public LaunchPadSecretFields()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, ISettingDefinition>(comparer);
        }

        public virtual bool AddField(string key, ISettingDefinition value)
        {
            Guard.AgainstNullOrEmpty(key, nameof(key));
            Guard.AgainstNull(value, nameof(value));
            if (Fields.ContainsKey(key))
            {
                return false;
            }
            Fields.Add(key, value);
            return true;
        }

        public virtual bool UpdateField(string key, ISettingDefinition value)
        {
            Guard.AgainstNullOrEmpty(key, nameof(key));
            Guard.AgainstNull(value, nameof(value));
            if (!Fields.ContainsKey(key))
            {
                return false;
            }
            Fields[key] = value;
            return true;
        }

        public virtual bool RemoveField(string key, ISettingDefinition value)
        {
            Guard.AgainstNullOrEmpty(key, nameof(key));
            Guard.AgainstNull(value, nameof(value));
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
        public virtual ISettingDefinition GetValue(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            return GetValueAsync(key, caller, keyIsCaseInsensitive).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>System.String.</returns>
        public virtual async Task<ISettingDefinition> GetValueAsync(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            ISettingDefinition value = null;
            if (keyIsCaseInsensitive)
            {
                value = Fields.FirstOrDefault(k => k.Key.ToLower() == key.ToLower()).Value;
            }
            else
            {
                value = Fields.FirstOrDefault(k => k.Key == key).Value;
            }
            return await Task.FromResult(value);
        }

        /// <summary>
        /// Finds the values for keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>IDictionary&lt;System.String, ISettingDefinition&gt;.</returns>
        public virtual IDictionary<string, ISettingDefinition> FindValuesForKeys(IList<string> keys, string caller, bool keyIsCaseInsensitive = true)
        {
            IDictionary<string, ISettingDefinition> kvps = new Dictionary<string, ISettingDefinition>();
            // loop through the desired set of keys to find the corresponding values in the JSON
            if (keyIsCaseInsensitive)
            {
                kvps = (IDictionary<string, ISettingDefinition>)keys.Where(k => Fields.ContainsKey(k.ToLower()));
            }
            else
            {
                kvps = (IDictionary<string, ISettingDefinition>)keys.Where(k => Fields.ContainsKey(k));
            }

            return kvps;
        }

    }
}
