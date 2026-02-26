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

using System.Threading.Tasks;
using Deploy.LaunchPad.Core.Runtime.Session;

namespace Deploy.LaunchPad.Core.Application.Features
{
    /// <summary>
    /// This interface should be used to get the value of features
    /// </summary>
    public partial interface IFeatureChecker
    {
        /// <summary>
        /// Gets the value of a feature by its name.
        /// This is a shortcut for <see cref="GetValueAsync(int, string)"/> that uses <see cref="ILaunchPadSession.TenantId"/> as tenantId.
        /// Note: This method should only be used if a TenantId can be obtained from the session.
        /// </summary>
        /// <param name="name">Unique feature name</param>
        /// <returns>Feature's current value</returns>
        Task<string> GetValueAsync(string name);

        /// <summary>
        /// Gets the value of a feature by its name.
        /// This is a shortcut for <see cref="GetValue(int, string)"/> that uses <see cref="ILaunchPadSession.TenantId"/> as tenantId.
        /// Note: This method should only be used if a TenantId can be obtained from the session.
        /// </summary>
        /// <param name="name">Unique feature name</param>
        /// <returns>Feature's current value</returns>
        string GetValue(string name);

        /// <summary>
        /// Gets the value of a feature for a tenant by the feature's name.
        /// </summary>
        /// <param name="tenantId">Tenant's Id</param>
        /// <param name="name">Unique feature name</param>
        /// <returns>Feature's current value</returns>
        Task<string> GetValueAsync(System.Guid tenantId, string name);

        /// <summary>
        /// Gets the value of a feature for a tenant by the feature's name.
        /// </summary>
        /// <param name="tenantId">Tenant's Id</param>
        /// <param name="name">Unique feature name</param>
        /// <returns>Feature's current value</returns>
        string GetValue(System.Guid tenantId, string name);

        /// <summary>
        /// Checks if a given feature is enabled.
        /// This should be used for boolean-value features.
        /// 
        /// This is a shortcut for <see cref="IsEnabledAsync(int, string)"/> that uses <see cref="ILaunchPadSession.TenantId"/>.
        /// Note: This method should be used only if the TenantId can be obtained from the session.
        /// </summary>
        /// <param name="featureName">Unique feature name</param>
        /// <returns>True, if the current feature's value is "true".</returns>
        Task<bool> IsEnabledAsync(string featureName);

        /// <summary>
        /// Checks if a given feature is enabled.
        /// This should be used for boolean-value features.
        /// 
        /// This is a shortcut for <see cref="IsEnabled(int, string)"/> that uses <see cref="ILaunchPadSession.TenantId"/>.
        /// Note: This method should be used only if the TenantId can be obtained from the session.
        /// </summary>
        /// <param name="featureName">Unique feature name</param>
        /// <returns>True, if the current feature's value is "true".</returns>
        bool IsEnabled(string featureName);

        /// <summary>
        /// Checks if a given feature is enabled.
        /// This should be used for boolean-value features.
        /// </summary>
        /// <param name="tenantId">Tenant's Id</param>
        /// <param name="featureName">Unique feature name</param>
        /// <returns>True, if the current feature's value is "true".</returns>
        Task<bool> IsEnabledAsync(System.Guid tenantId, string featureName);

        /// <summary>
        /// Checks if a given feature is enabled.
        /// This should be used for boolean-value features.
        /// </summary>
        /// <param name="tenantId">Tenant's Id</param>
        /// <param name="featureName">Unique feature name</param>
        /// <returns>True, if the current feature's value is "true".</returns>
        bool IsEnabled(System.Guid tenantId, string featureName);
    }
}