using Deploy.LaunchPad.FactoryLite.CommandLine;
using Deploy.LaunchPad.Util.Helpers;
using Deploy.LaunchPad.Util.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{
    public interface ICommand
    {
        public ElementNameLight Name { get; init; }
        public ElementDescriptionLight Description { get; init; }
        IReadOnlyList<OptionDefinition> Options { get; }

        ErrorHandlingHelper ErrorHandlingHelper { get; init; }

        /// <summary>Execute the command with parsed/typed options.</summary>
        public Task<LaunchPadMethodResult<TResultValue>> ExecuteAsync<TCommand,TResultValue>(CommandInput input)
            where TCommand : Deploy.LaunchPad.Util.CommandLine.ICommand
            where TResultValue : class, ILaunchPadMethodResultValue
        ;

    }
}
