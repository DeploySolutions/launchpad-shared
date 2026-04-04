using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Notifications
{
    public partial interface IUserNotification
    {
        ITenantNotification Notification { get; set; }
        UserNotificationState State { get; set; }
        string TargetNotifiers { get; set; }
        List<string> TargetNotifiersList { get; }
        Guid? TenantId { get; set; }
        Guid UserId { get; set; }
    }
}