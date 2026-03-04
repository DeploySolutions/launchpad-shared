using Deploy.LaunchPad.Core.Application.Features;
using Deploy.LaunchPad.Core.Authorization;
using Deploy.LaunchPad.Core.Localization;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Notifications
{
    public interface INotificationDefinition
    {
        object this[string key] { get; set; }

        IDictionary<string, object> Attributes { get; }
        ILocalizableString Description { get; set; }
        ILocalizableString DisplayName { get; set; }
        Type EntityType { get; }
        IFeatureDependency FeatureDependency { get; set; }
        string Name { get; }
        IPermissionDependency PermissionDependency { get; set; }
    }
}