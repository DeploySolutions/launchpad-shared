namespace Deploy.LaunchPad.Core.Notifications
{
    public interface INotificationProvider
    {
        void SetNotifications(INotificationDefinitionContext context);
    }
}