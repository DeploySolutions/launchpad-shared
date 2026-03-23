using Deploy.LaunchPad.Util.Metadata;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Notifications
{
    public partial interface IUserNotificationInfo : IMustHaveId<Guid>
    {
        DateTime CreationTime { get; set; }
        UserNotificationState State { get; set; }
        string TargetNotifiers { get; set; }
        List<string> TargetNotifiersList { get; }
        Guid? TenantId { get; set; }
        Guid TenantNotificationId { get; set; }
        Guid UserId { get; set; }

        void SetTargetNotifiers(List<string> list);
    }
}