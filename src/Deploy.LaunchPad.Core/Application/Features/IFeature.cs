using Deploy.LaunchPad.Core.Localization;
using Deploy.LaunchPad.Core.UI.Inputs;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Application.Features
{
    public partial interface IFeature
    {
        object this[string key] { get; set; }

        IDictionary<string, object> Attributes { get; }
        IReadOnlyList<IFeature> Children { get; }
        string DefaultValue { get; set; }
        ILocalizableString Description { get; set; }
        ILocalizableString DisplayName { get; set; }
        IInputType InputType { get; set; }
        string Name { get; }
        IFeature Parent { get; }
        FeatureScopes Scope { get; set; }

        IFeature CreateChildFeature(string name, string defaultValue, ILocalizableString displayName = null, ILocalizableString description = null, FeatureScopes scope = FeatureScopes.All, IInputType inputType = null);
        void RemoveChildFeature(string name);
        string ToString();
    }
}