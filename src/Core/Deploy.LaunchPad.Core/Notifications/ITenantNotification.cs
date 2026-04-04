using System;

namespace Deploy.LaunchPad.Core.Notifications
{
    public partial interface ITenantNotification
    {
        DateTime CreationTime { get; set; }
        INotificationData Data { get; set; }
        object EntityId { get; set; }
        string EntityTypeName { get; set; }
        string NotificationName { get; set; }
        NotificationSeverity Severity { get; set; }
        Guid? TenantId { get; set; }
    }
}