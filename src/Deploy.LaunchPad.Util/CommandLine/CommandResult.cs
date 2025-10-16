using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{
    public partial class CommandResult : ICommandResult
    {
        public virtual bool Success { get; set; } = false;
        public virtual string? Message { get; set; }
        public virtual object? Data { get; set; }
    }
}
