using Deploy.LaunchPad.Core.Metadata;
using System;

namespace Deploy.LaunchPad.Core.Notifications
{
    public interface ITenantNotificationInfo : IMustHaveId<Guid>, IMustHaveCreationTime
    {
        string Data { get; set; }
        string DataTypeName { get; set; }
        string EntityId { get; set; }
        string EntityTypeAssemblyQualifiedName { get; set; }
        string EntityTypeName { get; set; }
        string NotificationName { get; set; }
        NotificationSeverity Severity { get; set; }
        Guid? TenantId { get; set; }
    }
}