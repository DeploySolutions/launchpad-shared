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
using System.Threading.Tasks;
using Deploy.LaunchPad.Core.Domain.Entities;
using Deploy.LaunchPad.Core.Runtime.Users;

namespace Deploy.LaunchPad.Core.Notifications
{
    /// <summary>
    /// Used to publish notifications.
    /// </summary>
    public interface INotificationPublisher
    {
        /// <summary>
        /// Publishes a new notification.
        /// </summary>
        /// <param name="notificationName">Unique notification name</param>
        /// <param name="data">Notification data (optional)</param>
        /// <param name="entityIdentifier">The entity identifier if this notification is related to an entity</param>
        /// <param name="severity">Notification severity</param>
        /// <param name="userIds">
        /// Target user id(s). 
        /// Used to send notification to specific user(s) (without checking the subscription). 
        /// If this is null/empty, the notification is sent to subscribed users.
        /// </param>
        /// <param name="excludedUserIds">
        /// Excluded user id(s).
        /// This can be set to exclude some users while publishing notifications to subscribed users.
        /// It's normally not set if <paramref name="userIds"/> is set.
        /// </param>
        /// <param name="tenantIds">
        /// Target tenant id(s).
        /// Used to send notification to subscribed users of specific tenant(s).
        /// This should not be set if <paramref name="userIds"/> is set.
        /// <see cref="NotificationPublisher.AllTenants"/> can be passed to indicate all tenants.
        /// But this can only work in a single database approach (all tenants are stored in host database).
        /// If this is null, then it's automatically set to the current tenant on <see cref="ILaunchPadSession.TenantId"/>. 
        /// </param>
        /// <param name="targetNotifiers">Which realtime notifiers should handle this notification. Given notifier must be added to the INotificationConfiguration.Notifiers</param>
        Task PublishAsync(
            string notificationName,
            INotificationData data = null,
            EntityIdentifier entityIdentifier = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            UserIdentifier[] userIds = null,
            UserIdentifier[] excludedUserIds = null,
            Guid?[] tenantIds = null,
            Type[] targetNotifiers = null);
    }
}
