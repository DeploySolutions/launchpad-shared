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

using System.Collections.Generic;
using System.Threading.Tasks;
using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Core.Runtime.Users;

namespace Deploy.LaunchPad.Core.Notifications
{
    /// <summary>
    /// Used to manage subscriptions for notifications.
    /// </summary>
    public interface INotificationSubscriptionManager
    {
        /// <summary>
        /// Subscribes to a notification for given user and notification informations.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        /// <param name="targetNotifiers">target notifier</param>
        Task SubscribeAsync(IUserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null, string targetNotifiers = null);

        /// <summary>
        /// Subscribes to a notification for given user and notification informations.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        /// <param name="targetNotifiers">target notifier</param>
        void Subscribe(IUserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null, string targetNotifiers = null);

        /// <summary>
        /// Subscribes to all available notifications for given user.
        /// It does not subscribe entity related notifications.
        /// </summary>
        /// <param name="user">User.</param>
        Task SubscribeToAllAvailableNotificationsAsync(IUserIdentifier user);

        /// <summary>
        /// Subscribes to all available notifications for given user.
        /// It does not subscribe entity related notifications.
        /// </summary>
        /// <param name="user">User.</param>
        void SubscribeToAllAvailableNotifications(IUserIdentifier user);

        /// <summary>
        /// Unsubscribes from a notification.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task UnsubscribeAsync(IUserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Unsubscribes from a notification.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        void Unsubscribe(IUserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Gets all subscribtions for given notification (including all tenants).
        /// This only works for single database approach in a multitenant application!
        /// </summary>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        /// <param name="targetNotifiers">target notifier</param>
        Task<List<INotificationSubscription>> GetSubscriptionsAsync(string notificationName, EntityIdentifier entityIdentifier = null, string targetNotifiers = null);

        /// <summary>
        /// Gets all subscribtions for given notification (including all tenants).
        /// This only works for single database approach in a multitenant application!
        /// </summary>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        /// <param name="targetNotifiers">target notifier</param>
        List<INotificationSubscription> GetSubscriptions(string notificationName, EntityIdentifier entityIdentifier = null, string targetNotifiers = null);

        /// <summary>
        /// Gets all subscribtions for given notification.
        /// </summary>
        /// <param name="tenantId">Tenant id. Null for the host.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        /// <param name="targetNotifiers">target notifier</param>
        Task<List<INotificationSubscription>> GetSubscriptionsAsync(System.Guid? tenantId, string notificationName, EntityIdentifier entityIdentifier = null, string targetNotifiers = null);

        /// <summary>
        /// Gets all subscribtions for given notification.
        /// </summary>
        /// <param name="tenantId">Tenant id. Null for the host.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        /// <param name="targetNotifiers">target notifier</param>
        List<INotificationSubscription> GetSubscriptions(System.Guid? tenantId, string notificationName, EntityIdentifier entityIdentifier = null, string targetNotifiers = null);

        /// <summary>
        /// Gets subscribed notifications for a user.
        /// </summary>
        /// <param name="user">User.</param>
        Task<List<INotificationSubscription>> GetSubscribedNotificationsAsync(IUserIdentifier user);

        /// <summary>
        /// Gets subscribed notifications for a user.
        /// </summary>
        /// <param name="user">User.</param>
        List<INotificationSubscription> GetSubscribedNotifications(IUserIdentifier user);

        /// <summary>
        /// Checks if a user subscribed for a notification.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        /// <param name="targetNotifiers">target notifier</param>
        Task<bool> IsSubscribedAsync(IUserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null, string targetNotifiers = null);

        /// <summary>
        /// Checks if a user subscribed for a notification.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        /// <param name="targetNotifiers">target notifier</param>
        bool IsSubscribed(IUserIdentifier user, string notificationName, EntityIdentifier entityIdentifier = null, string targetNotifiers = null);
    }
}
