namespace Deploy.LaunchPad.Core.Notifications
{
    public partial interface INotificationProvider
    {
        void SetNotifications(INotificationDefinitionContext context);
    }
}