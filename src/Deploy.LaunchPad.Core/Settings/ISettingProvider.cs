using Deploy.LaunchPad.Util.Dependency;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Configuration
{
    public interface ISettingProvider : ITransientDependency
    {
        IEnumerable<ISettingDefinition> GetSettingDefinitions(ISettingDefinitionProviderContext context);
    }
}