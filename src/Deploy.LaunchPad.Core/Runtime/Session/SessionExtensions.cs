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

using Deploy.LaunchPad.Core.Runtime.Session;
using Deploy.LaunchPad.Core.Runtime.Users;
using Deploy.LaunchPad.Util;
using System;

namespace Deploy.LaunchPad.Core.Runtime.Session
{
    /// <summary>
    /// Extension methods for <see cref="ILaunchPadSession"/>.
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Gets current User's Id.
        /// Throws <see cref="AbpException"/> if <see cref="ILaunchPadSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current User's Id.</returns>
        public static Guid GetUserId(this ILaunchPadSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new LaunchPadException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId.Value;
        }

        /// <summary>
        /// Gets current Tenant's Id.
        /// Throws <see cref="AbpException"/> if <see cref="ILaunchPadSession.TenantId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current Tenant's Id.</returns>
        /// <exception cref="AbpException"></exception>
        public static Guid GetTenantId(this ILaunchPadSession session)
        {
            if (!session.TenantId.HasValue)
            {
                throw new LaunchPadException("Session.TenantId is null! Possible problems: No user logged in or current logged in user in a host user (TenantId is always null for host users).");
            }

            return session.TenantId.Value;
        }

        /// <summary>
        /// Creates <see cref="UserIdentifier"/> from given session.
        /// Returns null if <see cref="ILaunchPadSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">The session.</param>
        public static IUserIdentifier ToUserIdentifier(this ILaunchPadSession session)
        {
            return session.UserId == null
                ? null
                : new UserIdentifier(session.TenantId, session.GetUserId());
        }
    }
}