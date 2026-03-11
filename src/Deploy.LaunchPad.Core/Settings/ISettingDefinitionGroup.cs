using Deploy.LaunchPad.Core.Localization;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Configuration
{
    public interface ISettingDefinitionGroup
    {
        IReadOnlyList<ISettingDefinitionGroup> Children { get; }
        ILocalizableString DisplayName { get; }
        string Name { get; }
        ISettingDefinitionGroup Parent { get;}

        ISettingDefinitionGroup AddChild(ISettingDefinitionGroup child);
    }
}