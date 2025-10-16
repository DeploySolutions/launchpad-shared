using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{
    public interface ICliCommand
    {
        string Name { get; }
        string Description { get; }
        IReadOnlyList<OptionDefinition> Options { get; }

        /// <summary>Execute the command with parsed/typed options.</summary>
        Task<TOutput> ExecuteAsync<TOutput>(CliParseResult args, IServiceProvider services, CancellationToken ct)
            where TOutput: class, new();            
    }
}
