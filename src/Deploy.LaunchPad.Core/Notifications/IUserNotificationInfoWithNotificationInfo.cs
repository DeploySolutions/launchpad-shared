namespace Deploy.LaunchPad.Core.Notifications
{
    public interface IUserNotificationInfoWithNotificationInfo
    {
        ITenantNotificationInfo Notification { get; set; }
        IUserNotificationInfo UserNotification { get; set; }
    }
}