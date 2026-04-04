using Deploy.LaunchPad.Code.Methods;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.CommandLine
{
    
    public partial class CommandResult<TResultValue> : LaunchPadMethodResult<TResultValue>, ICommandResult
        where TResultValue : LaunchPadMethodResultValueBase, ILaunchPadMethodResultValue
    {
        public CommandResult(Result<TResultValue> result) : base(result)
        {
        }
    }
}
