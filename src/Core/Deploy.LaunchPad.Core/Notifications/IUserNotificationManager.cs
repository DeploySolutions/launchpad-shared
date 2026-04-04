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

using Deploy.LaunchPad.Core.Runtime.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Notifications
{
    /// <summary>
    /// Used to manage user notifications.
    /// </summary>
    public partial interface IUserNotificationManager
    {
        /// <summary>
        /// Gets notifications for a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        Task<List<IUserNotification>> GetUserNotificationsAsync(IUserIdentifier user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets notifications for a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        List<IUserNotification> GetUserNotifications(IUserIdentifier user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets user notification count.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        Task<long> GetUserNotificationCountAsync(IUserIdentifier user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets user notification count.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        long GetUserNotificationCount(IUserIdentifier user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets a user notification by given id.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="userNotificationId">The user notification id.</param>
        Task<IUserNotification> GetUserNotificationAsync(System.Guid? tenantId, Guid userNotificationId);

        /// <summary>
        /// Gets a user notification by given id.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="userNotificationId">The user notification id.</param>
        IUserNotification GetUserNotification(System.Guid? tenantId, Guid userNotificationId);

        /// <summary>
        /// Updates a user notification state.
        /// </summary>
        /// <param name="tenantId">Tenant Id.</param>
        /// <param name="userNotificationId">The user notification id.</param>
        /// <param name="state">New state.</param>
        Task UpdateUserNotificationStateAsync(System.Guid? tenantId, Guid userNotificationId, UserNotificationState state);

        /// <summary>
        /// Updates a user notification state.
        /// </summary>
        /// <param name="tenantId">Tenant Id.</param>
        /// <param name="userNotificationId">The user notification id.</param>
        /// <param name="state">New state.</param>
        void UpdateUserNotificationState(System.Guid? tenantId, Guid userNotificationId, UserNotificationState state);

        /// <summary>
        /// Updates all notification states for a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">New state.</param>
        Task UpdateAllUserNotificationStatesAsync(IUserIdentifier user, UserNotificationState state);

        /// <summary>
        /// Updates all notification states for a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">New state.</param>
        void UpdateAllUserNotificationStates(IUserIdentifier user, UserNotificationState state);

        /// <summary>
        /// Deletes a user notification.
        /// </summary>
        /// <param name="tenantId">Tenant Id.</param>
        /// <param name="userNotificationId">The user notification id.</param>
        Task DeleteUserNotificationAsync(System.Guid? tenantId, Guid userNotificationId);

        /// <summary>
        /// Deletes a user notification.
        /// </summary>
        /// <param name="tenantId">Tenant Id.</param>
        /// <param name="userNotificationId">The user notification id.</param>
        void DeleteUserNotification(System.Guid? tenantId, Guid userNotificationId);

        /// <summary>
        /// Deletes all notifications of a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State</param>
        /// <param name="startDate">Delete notifications published after startDateTime</param>
        /// <param name="endDate">Delete notifications published before startDateTime</param>
        Task DeleteAllUserNotificationsAsync(IUserIdentifier user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Deletes all notifications of a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State</param>
        /// <param name="startDate">Delete notifications published after startDateTime</param>
        /// <param name="endDate">Delete notifications published before startDateTime</param>
        void DeleteAllUserNotifications(IUserIdentifier user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}