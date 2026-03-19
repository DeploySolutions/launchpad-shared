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

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Deploy.LaunchPad.Core.Application.Features;
using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.MultiTenancy;

namespace Deploy.LaunchPad.Core.Authorization
{
    /// <summary>
    /// Represents a permission.
    /// A permission is used to restrict functionalities of the application from unauthorized users.
    /// </summary>
    public partial class Permission : IPermission
    {
        /// <summary>
        /// Parent of this permission if one exists.
        /// If set, this permission can be granted only if parent is granted.
        /// </summary>
        public IPermission Parent { get; private set; }

        /// <summary>
        /// Unique name of the permission.
        /// This is the key name to grant permissions.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Display name of the permission.
        /// This can be used to show permission to the user.
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// A brief description for this permission.
        /// </summary>
        public ILocalizableString Description { get; set; }

        /// <summary>
        /// Which side can use this permission.
        /// </summary>
        public MultiTenancySides MultiTenancySides { get; set; }

        /// <summary>
        /// Depended feature(s) of this permission.
        /// </summary>
        public IFeatureDependency FeatureDependency { get; set; }

        /// <summary>
        /// Custom Properties. Use this to add your own properties to permission.
        /// <para>You can use this with indexer like Permission["mykey"]=data; </para>
        /// <para>object mydata=Permission["mykey"]; </para>
        /// </summary>
        public Dictionary<string, object> Properties { get; }

        /// <summary>
        /// Shortcut of Properties dictionary
        /// </summary>
        public object this[string key]
        {
            get => !Properties.ContainsKey(key) ? null : Properties[key];
            set
            {
                Properties[key] = value;
            }
        }
        /// <summary>
        /// List of child permissions. A child permission can be granted only if parent is granted.
        /// </summary>
        public IReadOnlyList<IPermission> Children => _children.ToImmutableList();
        private readonly List<IPermission> _children;

        /// <summary>
        /// Creates a new Permission.
        /// </summary>
        /// <param name="name">Unique name of the permission</param>
        /// <param name="displayName">Display name of the permission</param>
        /// <param name="description">A brief description for this permission</param>
        /// <param name="multiTenancySides">Which side can use this permission</param>
        /// <param name="featureDependency">Depended feature(s) of this permission</param>
        /// <param name="properties">Custom Properties. Use this to add your own properties to permission.</param>
        public Permission(
            string name,
            ILocalizableString displayName = null,
            ILocalizableString description = null,
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant,
            IFeatureDependency featureDependency = null,
            Dictionary<string, object> properties = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            DisplayName = displayName;
            Description = description;
            MultiTenancySides = multiTenancySides;
            FeatureDependency = featureDependency;
            Properties = properties ?? new Dictionary<string, object>();

            _children = new List<IPermission>();
        }

        /// <summary>
        /// Adds a child permission.
        /// A child permission can be granted only if parent is granted.
        /// </summary>
        /// <returns>Returns newly created child permission</returns>
        public IPermission CreateChildPermission(
            string name,
            ILocalizableString displayName = null,
            ILocalizableString description = null,
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant,
            IFeatureDependency featureDependency = null,
            Dictionary<string, object> properties = null)
        {
            var permission = new Permission(name, displayName, description, multiTenancySides, featureDependency, properties) { Parent = this };
            _children.Add(permission);
            return permission;
        }

        public void RemoveChildPermission(string name)
        {
            _children.RemoveAll(p => p.Name == name);
        }

        public override string ToString()
        {
            return string.Format("[Permission: {0}]", Name);
        }
    }
}
