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


using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Core.Metadata;

namespace Deploy.LaunchPad.Core.Domain.Entities.Auditing
{
    /// <summary>
    /// This interface is implemented by entities that is wanted to store modification information (who and when modified lastly).
    /// Properties are automatically set when updating the <see cref="IEntity"/>.
    /// </summary>
    public interface IModificationAudited : IHasModificationTime
    {
        /// <summary>
        /// Last modifier user for this entity.
        /// </summary>
        long? LastModifierUserId { get; set; }
    }

    /// <summary>
    /// Adds navigation properties to <see cref="IModificationAudited"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IModificationAudited<TUser> : IModificationAudited
        where TUser : IEntity<long>
    {
        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        TUser LastModifierUser { get; set; }
    }
}