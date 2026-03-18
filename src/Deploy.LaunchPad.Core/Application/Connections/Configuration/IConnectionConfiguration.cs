using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Application.Connections.Configuration
{
    public partial interface IConnectionConfiguration
    {
        IReadOnlyList<ILaunchPadConnectionDefinition> Connections { get; }

        ILaunchPadConnectionDefinition? GetOrNull(string name);
    }
}
