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
        IReadOnlyList<OptionDefinition> Options { get; init; }

        /// <summary>Execute the command with parsed/typed options.</summary>
        public Task<ICommandResult> ExecuteAsync<TOutput>(CliParseResult args, IServiceProvider services, CancellationToken ct);
    }
}
