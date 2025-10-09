using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{

    public sealed record OptionDefinition(
        string Name,                          // long name: "name"
        char? ShortName,                      // short: 'n'  (use null if none)
        OptionArity Arity,                    // None or One
        bool Required,                        // whether required
        Type ValueType,                       // typeof(string), typeof(bool), typeof(int), typeof(FileInfo), etc.
        string Description,
        object? DefaultValue = null           // used when not provided (non-required)
    )
    {
        public string LongSwitch => $"--{Name}";
        public string? ShortSwitch => ShortName is char c ? $"-{c}" : null;
    }

}
