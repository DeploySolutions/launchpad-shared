using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Code.CommandLine
{
    public partial class BatchCommand
    {
        public virtual string command { get; set; } = string.Empty;
        public virtual Dictionary<string, object>? args { get; set; }

        public BatchCommand() { }
    }
}
