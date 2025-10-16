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
        public ElementNameLight Name { get; }
        public ElementDescriptionLight Description { get; }
        IReadOnlyList<OptionDefinition> Options { get; }

        /// <summary>Execute the command with parsed/typed options.</summary>
        public Task<CommandResult> ExecuteAsync<TOutput>(CliParseResult args, IServiceProvider services, CancellationToken ct);
    }
}
