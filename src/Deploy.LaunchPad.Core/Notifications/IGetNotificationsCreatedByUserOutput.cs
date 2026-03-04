using System;

namespace Deploy.LaunchPad.Core.Notifications
{
    public interface IGetNotificationsCreatedByUserOutput
    {
        DateTime CreationTime { get; set; }
        string Data { get; set; }
        string DataTypeName { get; set; }
        bool IsPublished { get; set; }
        string NotificationName { get; set; }
        NotificationSeverity Severity { get; set; }
    }
}