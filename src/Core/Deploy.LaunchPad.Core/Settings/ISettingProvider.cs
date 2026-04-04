using Deploy.LaunchPad.Util.Dependency;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Configuration
{
    public partial interface ISettingProvider : ITransientDependency
    {
        IEnumerable<ISettingDefinition> GetSettingDefinitions(ISettingDefinitionProviderContext context);
    }
}