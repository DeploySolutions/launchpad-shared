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
    /// Used to store (persist) notifications.
    /// </summary>
    public partial interface INotificationStore
    {
        /// <summary>
        /// Inserts a notification subscription.
        /// </summary>
        Task InsertSubscriptionAsync(INotificationSubscriptionInfo subscription);

        /// <summary>
        /// Inserts a notification subscription.
        /// </summary>
        void InsertSubscription(INotificationSubscriptionInfo subscription);

        /// <summary>
        /// Deletes a notification subscription.
        /// </summary>
        Task DeleteSubscriptionAsync(IUserIdentifier user, string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Deletes a notification subscription.
        /// </summary>
        void DeleteSubscription(IUserIdentifier user, string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Inserts a notification.
        /// </summary>
        Task InsertNotificationAsync(INotificationInfo notification);

        /// <summary>
        /// Inserts a notification.
        /// </summary>
        void InsertNotification(INotificationInfo notification);

        /// <summary>
        /// Gets a notification by Id, or returns null if not found.
        /// </summary>
        Task<INotificationInfo> GetNotificationOrNullAsync(Guid notificationId);

        /// <summary>
        /// Gets a notification by Id, or returns null if not found.
        /// </summary>
        INotificationInfo GetNotificationOrNull(Guid notificationId);

        /// <summary>
        /// Inserts a user notification.
        /// </summary>
        Task InsertUserNotificationAsync(IUserNotificationInfo userNotification);

        /// <summary>
        /// Inserts a user notification.
        /// </summary>
        void InsertUserNotification(IUserNotificationInfo userNotification);

        /// <summary>
        /// Gets subscriptions for a notification.
        /// </summary>
        Task<List<INotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, string entityTypeName, string entityId, string targetNotifiers);

        /// <summary>
        /// Gets subscriptions for a notification.
        /// </summary>
        List<INotificationSubscriptionInfo> GetSubscriptions(string notificationName, string entityTypeName, string entityId, string targetNotifiers);

        /// <summary>
        /// Gets subscriptions for a notification for specified tenant(s).
        /// </summary>
        Task<List<INotificationSubscriptionInfo>> GetSubscriptionsAsync(Guid?[] tenantIds, string notificationName, string entityTypeName, string entityId, string targetNotifiers);

        /// <summary>
        /// Gets subscriptions for a notification for specified tenant(s).
        /// </summary>
        List<INotificationSubscriptionInfo> GetSubscriptions(Guid?[] tenantIds, string notificationName, string entityTypeName, string entityId, string targetNotifiers);

        /// <summary>
        /// Gets subscriptions for a user.
        /// </summary>
        Task<List<INotificationSubscriptionInfo>> GetSubscriptionsAsync(IUserIdentifier user);

        /// <summary>
        /// Gets subscriptions for a user.
        /// </summary>
        List<INotificationSubscriptionInfo> GetSubscriptions(IUserIdentifier user);

        /// <summary>
        /// Checks if a user subscribed for a notification
        /// </summary>
        Task<bool> IsSubscribedAsync(IUserIdentifier user, string notificationName, string entityTypeName, string entityId, string targetNotifiers);

        /// <summary>
        /// Checks if a user subscribed for a notification
        /// </summary>
        bool IsSubscribed(IUserIdentifier user, string notificationName, string entityTypeName, string entityId, string targetNotifiers);

        /// <summary>
        /// Updates a user notification state.
        /// </summary>
        Task UpdateUserNotificationStateAsync(System.Guid? tenantId, Guid userNotificationId, UserNotificationState state);

        /// <summary>
        /// Updates a user notification state.
        /// </summary>
        void UpdateUserNotificationState(System.Guid? tenantId, Guid userNotificationId, UserNotificationState state);

        /// <summary>
        /// Updates all notification states for a user.
        /// </summary>
        Task UpdateAllUserNotificationStatesAsync(IUserIdentifier user, UserNotificationState state);

        /// <summary>
        /// Updates all notification states for a user.
        /// </summary>
        void UpdateAllUserNotificationStates(IUserIdentifier user, UserNotificationState state);

        /// <summary>
        /// Deletes a user notification.
        /// </summary>
        Task DeleteUserNotificationAsync(System.Guid? tenantId, Guid userNotificationId);

        /// <summary>
        /// Deletes a user notification.
        /// </summary>
        void DeleteUserNotification(System.Guid? tenantId, Guid userNotificationId);

        /// <summary>
        /// Deletes all notifications of a user.
        /// </summary>
        Task DeleteAllUserNotificationsAsync(IUserIdentifier user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Deletes all notifications of a user.
        /// </summary>
        void DeleteAllUserNotifications(IUserIdentifier user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets notifications of a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        Task<List<IUserNotificationInfoWithNotificationInfo>> GetUserNotificationsWithNotificationsAsync(IUserIdentifier user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets notifications of a user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">State</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        List<IUserNotificationInfoWithNotificationInfo> GetUserNotificationsWithNotifications(IUserIdentifier user, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets user notification count.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">The state.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        Task<long> GetUserNotificationCountAsync(IUserIdentifier user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets user notification count.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="state">The state.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        long GetUserNotificationCount(IUserIdentifier user, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets a user notification.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="userNotificationId">Skip count.</param>
        Task<IUserNotificationInfoWithNotificationInfo> GetUserNotificationWithNotificationOrNullAsync(System.Guid? tenantId, Guid userNotificationId);

        /// <summary>
        /// Gets a user notification.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="userNotificationId">Skip count.</param>
        IUserNotificationInfoWithNotificationInfo GetUserNotificationWithNotificationOrNull(System.Guid? tenantId, Guid userNotificationId);

        /// <summary>
        /// Inserts notification for a tenant.
        /// </summary>
        Task InsertTenantNotificationAsync(ITenantNotificationInfo tenantNotificationInfo);

        /// <summary>
        /// Inserts notification for a tenant.
        /// </summary>
        void InsertTenantNotification(ITenantNotificationInfo tenantNotificationInfo);

        /// <summary>
        /// Deletes the notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        Task DeleteNotificationAsync(INotificationInfo notification);

        /// <summary>
        /// Deletes the notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        void DeleteNotification(INotificationInfo notification);

        Task<List<IGetNotificationsCreatedByUserOutput>> GetNotificationsPublishedByUserAsync(IUserIdentifier user,
            string notificationName, DateTime? startDate, DateTime? endDate);
    }
}