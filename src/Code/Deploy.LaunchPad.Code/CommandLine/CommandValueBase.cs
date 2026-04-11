using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Code.Methods;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Deploy.LaunchPad.Util.Metadata;

namespace Deploy.LaunchPad.Code.CommandLine
{
    public abstract partial class CommandValueBase : LaunchPadMethodResultValueBase, IMustHaveFullName
    {
        public required string Name { get; set; }

        [SetsRequiredMembers]
        public CommandValueBase(string name)
        {
            Name = name;
        }

    }
}
