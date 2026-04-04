#region "Licensing"
/*
 * SPDX-FileCopyrightText: Copyright (c) Volosoft (https://volosoft.com) and contributors
 * SPDX-License-Identifier: MIT
 *
 * This file contains code originally from the ASP.NET Boilerplate - Web Application Framework:
 *   Repository: https://github.com/aspnetboilerplate/aspnetboilerplate
 *
 * The original portions of this file remain licensed under the MIT License.
 * You may obtain a copy of the MIT License at:
 *
 *   https://opensource.org/license/mit
 *
 *
 * SPDX-FileCopyrightText: Copyright (c) 2026 Deploy Software Solutions (https://www.deploy.solutions)
 * SPDX-License-Identifier: Apache-2.0
 *
 * Modifications and additional code in this file are licensed under
 * the Apache License, Version 2.0.
 *
 * You may obtain a copy of the Apache License at:
 *
 *   https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the Apache License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *
 * See the applicable license for governing permissions and limitations.
 */
#endregion


using Deploy.LaunchPad.Util.Extensions;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Configuration
{
    /// <summary>
    /// Used to set/get custom configuration.
    /// </summary>
    public partial class DictionaryBasedConfiguration : IDictionaryBasedConfiguration
    {
        /// <summary>
        /// Dictionary of custom configuration.
        /// </summary>
        protected Dictionary<string, object> CustomSettings { get; private set; }

        /// <summary>
        /// Gets/sets a config value.
        /// Returns null if no config with given name.
        /// </summary>
        /// <param name="name">Name of the config</param>
        /// <returns>Value of the config</returns>
        public virtual object this[string name]
        {
            get { return CustomSettings.GetOrDefault(name); }
            set { CustomSettings[name] = value; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected DictionaryBasedConfiguration()
        {
            CustomSettings = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets a configuration value as a specific type.
        /// </summary>
        /// <param name="name">Name of the config</param>
        /// <typeparam name="T">Type of the config</typeparam>
        /// <returns>Value of the configuration or null if not found</returns>
        public virtual T Get<T>(string name)
        {
            var value = this[name];
            return value == null
                ? default(T)
                : (T) Convert.ChangeType(value, typeof (T));
        }

        /// <summary>
        /// Used to set a string named configuration.
        /// If there is already a configuration with same <paramref name="name"/>, it's overwritten.
        /// </summary>
        /// <param name="name">Unique name of the configuration</param>
        /// <param name="value">Value of the configuration</param>
        public virtual void Set<T>(string name, T value)
        {
            this[name] = value;
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// </summary>
        /// <param name="name">Unique name of the configuration</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public virtual object Get(string name)
        {
            return Get(name, null);
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// </summary>
        /// <param name="name">Unique name of the configuration</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public virtual object Get(string name, object defaultValue)
        {
            var value = this[name];
            if (value == null)
            {
                return defaultValue;
            }

            return this[name];
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="name">Unique name of the configuration</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public virtual T Get<T>(string name, T defaultValue)
        {
            return (T)Get(name, (object)defaultValue);
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="name">Unique name of the configuration</param>
        /// <param name="creator">The function that will be called to create if given configuration is not found</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public virtual T GetOrCreate<T>(string name, Func<T> creator)
        {
            var value = Get(name);
            if (value == null)
            {
                value = creator();
                Set(name, value);
            }
            return (T) value;
        }
    }
}