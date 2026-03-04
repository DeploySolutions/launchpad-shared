using System;

namespace Deploy.LaunchPad.Core.Notifications
{
    public interface INotificationSubscription
    {
        DateTime CreationTime { get; set; }
        object EntityId { get; set; }
        Type EntityType { get; set; }
        string EntityTypeName { get; set; }
        string NotificationName { get; set; }
        Guid? TenantId { get; set; }
        Guid UserId { get; set; }
    }
}