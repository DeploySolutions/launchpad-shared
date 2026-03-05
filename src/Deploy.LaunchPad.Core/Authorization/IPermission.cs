using Deploy.LaunchPad.Core.Application.Features;
using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.MultiTenancy;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Authorization
{
    public partial interface IPermission
    {
        object this[string key] { get; set; }

        IReadOnlyList<IPermission> Children { get; }
        ILocalizableString Description { get; set; }
        ILocalizableString DisplayName { get; set; }
        IFeatureDependency FeatureDependency { get; set; }
        MultiTenancySides MultiTenancySides { get; set; }
        string Name { get; }
        IPermission Parent { get; }
        Dictionary<string, object> Properties { get; }

        IPermission CreateChildPermission(string name, ILocalizableString displayName = null, ILocalizableString description = null, MultiTenancySides multiTenancySides = MultiTenancySides.Tenant | MultiTenancySides.Host, IFeatureDependency featureDependency = null, Dictionary<string, object> properties = null);
        void RemoveChildPermission(string name);
        string ToString();
    }
}