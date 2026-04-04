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
using System.Transactions;

namespace Deploy.LaunchPad.Core.Domain.UnitOfWork
{
    /// <summary>
    /// Used to get/set default options for a unit of work.
    /// </summary>
    public partial interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Scope option.
        /// </summary>
        TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// Should unit of works be transactional.
        /// Default: true.
        /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        /// A boolean value indicates that System.Transactions.TransactionScope is available for current application.
        /// Default: true.
        /// </summary>
        bool IsTransactionScopeAvailable { get; set; }

        /// <summary>
        /// Gets/sets a timeout value for unit of works.
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets/sets isolation level of transaction.
        /// This is used if <see cref="IsTransactional"/> is true.
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Gets list of all data filter configurations.
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }
        
        /// <summary>
        /// Gets list of all data filter configurations.
        /// </summary>
        IReadOnlyList<AuditFieldConfiguration> AuditFieldConfiguration { get; }

        /// <summary>
        /// A list of selectors to determine conventional Unit Of Work classes.
        /// </summary>
        List<Func<Type, bool>> ConventionalUowSelectors { get; }

        /// <summary>
        /// Registers a data filter to unit of work system.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default.</param>
        void RegisterFilter(string filterName, bool isEnabledByDefault);
        
        /// <summary>
        /// Registers an audit field configuration to unit of work system.
        /// </summary>
        /// <param name="fieldName">Name of the audit field.</param>
        /// <param name="isSavingEnabledByDefault">Is saving field enabled by default.</param>
        void RegisterAuditFieldConfiguration(string fieldName, bool isSavingEnabledByDefault);

        /// <summary>
        /// Overrides a data filter definition.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default.</param>
        void OverrideFilter(string filterName, bool isEnabledByDefault);
    }
}
