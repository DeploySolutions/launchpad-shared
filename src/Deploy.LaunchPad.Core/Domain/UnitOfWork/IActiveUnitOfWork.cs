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
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.UnitOfWork
{
    /// <summary>
    /// This interface is used to work with active unit of work.
    /// This interface can not be injected.
    /// Use <see cref="IUnitOfWorkManager"/> instead.
    /// </summary>
    public partial interface IActiveUnitOfWork
    {
        /// <summary>
        /// This event is raised when this UOW is successfully completed.
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// This event is raised when this UOW is failed.
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// This event is raised when this UOW is disposed.
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// Gets if this unit of work is transactional.
        /// </summary>
        UnitOfWorkOptions Options { get; }

        /// <summary>
        /// Gets data filter configurations for this unit of work.
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// Gets audit field configurations for this unit of work.
        /// </summary>
        IReadOnlyList<AuditFieldConfiguration> AuditFieldConfiguration { get; }
        
        /// <summary>
        /// A dictionary to use for custom operations on unitOfWork
        /// </summary>
        Dictionary<string, object> Items { get; set; }

        /// <summary>
        /// Is this UOW disposed?
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// This method may be called to apply changes whenever needed.
        /// Note that if this unit of work is transactional, saved changes are also rolled back if transaction is rolled back.
        /// No explicit call is needed to SaveChanges generally, 
        /// since all changes saved at end of a unit of work automatically.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// This method may be called to apply changes whenever needed.
        /// Note that if this unit of work is transactional, saved changes are also rolled back if transaction is rolled back.
        /// No explicit call is needed to SaveChanges generally, 
        /// since all changes saved at end of a unit of work automatically.
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Disables one or more data filters.
        /// Does nothing for a filter if it's already disabled. 
        /// Use this method in a using statement to re-enable filters if needed.
        /// </summary>
        /// <param name="filterNames">One or more filter names. <see cref="AbpDataFilters"/> for standard filters.</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the disable effect.</returns>
        IDisposable DisableFilter(params string[] filterNames);

        /// <summary>
        /// Enables one or more data filters.
        /// Does nothing for a filter if it's already enabled.
        /// Use this method in a using statement to re-disable filters if needed.
        /// </summary>
        /// <param name="filterNames">One or more filter names. <see cref="AbpDataFilters"/> for standard filters.</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the enable effect.</returns>
        IDisposable EnableFilter(params string[] filterNames);

        /// <summary>
        /// Checks if a filter is enabled or not.
        /// </summary>
        /// <param name="filterName">Name of the filter. <see cref="AbpDataFilters"/> for standard filters.</param>
        bool IsFilterEnabled(string filterName);

        /// <summary>
        /// Sets (overrides) value of a filter parameter.
        /// </summary>
        /// <param name="filterName">Name of the filter</param>
        /// <param name="parameterName">Parameter's name</param>
        /// <param name="value">Value of the parameter to be set</param>
        IDisposable SetFilterParameter(string filterName, string parameterName, object value);

        /// <summary>
        /// Disables automatic saving for one or more audit fields.
        /// </summary>
        /// <param name="fieldNames">One or more audit field names. <see cref="AbpAuditFields"/> for standard fields.</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the disable effect.</returns>
        IDisposable DisableAuditing(params string[] fieldNames);
        
        /// <summary>
        /// Enables automatic saving for one or more audit fields.
        /// </summary>
        /// <param name="fieldNames">One or more audit field names. <see cref="AbpAuditFields"/> for standard fields.</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the enable effect.</returns>
        IDisposable EnableAuditing(params string[] fieldNames);
        
        /// <summary>
        /// Sets/Changes Tenant's Id for this UOW.
        /// </summary>
        /// <param name="tenantId">The tenant id.</param>
        /// <returns>A disposable object to restore old TenantId value when you dispose it</returns>
        IDisposable SetTenantId(int? tenantId);

        /// <summary>
        /// Sets/Changes Tenant's Id for this UOW.
        /// </summary>
        /// <param name="tenantId">The tenant id</param>
        /// <param name="switchMustHaveTenantEnableDisable">
        /// True to enable/disable <see cref="IMustHaveTenant"/> based on given tenantId.
        /// Enables <see cref="IMustHaveTenant"/> filter if tenantId is not null.
        /// Disables <see cref="IMustHaveTenant"/> filter if tenantId is null.
        /// This value is true for <see cref="SetTenantId(int?)"/> method.
        /// </param>
        /// <returns>A disposable object to restore old TenantId value when you dispose it</returns>
        IDisposable SetTenantId(int? tenantId, bool switchMustHaveTenantEnableDisable);

        /// <summary>
        /// Gets Tenant Id for this UOW.
        /// </summary>
        /// <returns></returns>
        int? GetTenantId();
    }
}
