using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Notifications
{
    public interface INotificationInfo
    {
        string Data { get; set; }
        string DataTypeName { get; set; }
        string EntityId { get; set; }
        string EntityTypeAssemblyQualifiedName { get; set; }
        string EntityTypeName { get; set; }
        string ExcludedUserIds { get; set; }
        string NotificationName { get; set; }
        NotificationSeverity Severity { get; set; }
        string TargetNotifiers { get; set; }
        List<string> TargetNotifiersList { get; }
        string TenantIds { get; set; }
        string UserIds { get; set; }

        void SetTargetNotifiers(List<string> list);
    }
}