using Deploy.LaunchPad.Code.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Code.Helpers;
using Deploy.LaunchPad.Core.Elements;

namespace Deploy.LaunchPad.Code.CommandLine
{
    public interface ICommand
    {
        public ElementNameLight Name { get; init; }
        public ElementDescriptionLight Description { get; init; }
        IReadOnlyList<OptionDefinition> Options { get; }

        ErrorHandlingHelper ErrorHandlingHelper { get; init; }

       
        /// <summary>Execute the command with parsed/typed options.</summary>
        public Task<LaunchPadMethodResult<TResultValue>> ExecuteAsync<TCommand,TResultValue>(CommandInput input)
            where TCommand : ICommand
            where TResultValue : class, ILaunchPadMethodResultValue
        ;

    }
}
