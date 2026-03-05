namespace Deploy.LaunchPad.Core.Notifications
{
    public partial interface IUserNotificationInfoWithNotificationInfo
    {
        ITenantNotificationInfo Notification { get; set; }
        IUserNotificationInfo UserNotification { get; set; }
    }
}