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

using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.Secrets.References;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Deploy.LaunchPad.Core.Configuration
{
    /// <summary>
    /// Defines a setting.
    /// A setting is used to configure and change behavior of the application.
    /// </summary>
    [DebuggerDisplay("{_debugDisplay}")]
    public partial class SettingDefinition : SettingDefinitionBase
    {
        /// <summary>
        /// Controls the DebuggerDisplay attribute presentation (above). This will only appear during VS debugging sessions and should never be logged.
        /// </summary>
        /// <value>The debug display.</value>
        protected virtual string _debugDisplay => $"{Name}.{DefaultValue}";
        
        /// <summary>
        /// Creates a new <see cref="SettingDefinition"/> object.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <param name="defaultValue">Default value of the setting</param>
        /// <param name="displayName">Display name of the setting</param>
        /// <param name="group">Group of this setting</param>
        /// <param name="description">A brief description for this setting</param>
        /// <param name="scopes">Scopes of this setting. Default value: <see cref="SettingScopes.Application"/>.</param>
        /// <param name="isVisibleToClients">This parameter is obsolete. Use <paramref name="clientVisibilityProvider"/> instead! Default: false</param>
        /// <param name="isInherited">Is this setting inherited from parent scopes. Default: True.</param>
        /// <param name="customData">Can be used to store a custom object related to this setting</param>
        /// <param name="clientVisibilityProvider">Client visibility definition for the setting. Default: invisible</param>
        /// <param name="isEncrypted">Is this setting stored as encrypted in the data source.</param>
        public SettingDefinition(
            string name,
            string defaultValue,
            ILocalizableString displayName = null,
            ISettingDefinitionGroup group = null,
            ILocalizableString description = null,
            SettingScopes scopes = SettingScopes.Application,
            bool isVisibleToClients = false,
            bool isInherited = true,
            object customData = null,
            ISettingClientVisibilityProvider clientVisibilityProvider = null,
            bool isEncrypted = false,
            IReadOnlyList<ISecretFieldReference> secretSources = null
            )
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            DefaultValue = defaultValue;
            DisplayName = displayName;
            Group = @group;
            Description = description;
            Scopes = scopes;
            IsInherited = isInherited;
            CustomData = customData;
            IsEncrypted = isEncrypted;

            ClientVisibilityProvider = new HiddenSettingClientVisibilityProvider();

            if (isVisibleToClients)
            {
                ClientVisibilityProvider = new VisibleSettingClientVisibilityProvider();
            }
            else if (clientVisibilityProvider != null)
            {
                ClientVisibilityProvider = clientVisibilityProvider;
            }
            if (secretSources != null)
            {
                SecretSources = secretSources;
                // as a safety, always mark this as needing encryption if it is ever persisted and hide from client
                if (secretSources.Count > 0) 
                {
                    IsEncrypted = true;
                    ClientVisibilityProvider = new HiddenSettingClientVisibilityProvider();
                }
            }
        }
    }
}
