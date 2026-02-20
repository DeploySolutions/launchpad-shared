using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Code.Methods;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Deploy.LaunchPad.Core.Metadata;

namespace Deploy.LaunchPad.Code.CommandLine
{
    public abstract partial class CommandValueBase : LaunchPadMethodResultValueBase
    {
        public required LaunchPadMinimalProperties Core { get; init; }

        [SetsRequiredMembers]
        public CommandValueBase()
        {
            Core = new LaunchPadMinimalProperties();
        }

        [SetsRequiredMembers]
        public CommandValueBase(LaunchPadMinimalProperties minimalProperties)
        {
            Guard.Against<ArgumentNullException>(minimalProperties == null, "minimalProperties cannot be null.");
            Core = minimalProperties;
        }

    }
}
