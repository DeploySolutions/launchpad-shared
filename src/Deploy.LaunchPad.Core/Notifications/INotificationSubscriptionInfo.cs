using Deploy.LaunchPad.Util.Metadata;
using System;

namespace Deploy.LaunchPad.Core.Notifications
{
    public partial interface INotificationSubscriptionInfo : IMustHaveCreationTime, IMustHaveId<Guid>
    {
        string EntityId { get; set; }
        string EntityTypeAssemblyQualifiedName { get; set; }
        string EntityTypeName { get; set; }
        string NotificationName { get; set; }
        string TargetNotifiers { get; set; }
        Guid? TenantId { get; set; }
        Guid UserId { get; set; }
    }
}