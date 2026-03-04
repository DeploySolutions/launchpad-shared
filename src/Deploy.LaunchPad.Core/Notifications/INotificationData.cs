using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Notifications
{
    public interface INotificationData
    {
        object this[string key] { get; set; }

        Dictionary<string, object> Properties { get; set; }
        string Type { get; }

        string ToString();
    }
}