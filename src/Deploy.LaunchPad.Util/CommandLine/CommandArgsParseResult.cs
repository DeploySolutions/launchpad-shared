using Deploy.LaunchPad.Util.Methods;
using FluentResults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{
    /// <summary>
    /// Represents the result of parsing command-line arguments, encapsulating both the parsed values and utility methods for accessing them.
    /// </summary>
    /// <remarks>
    /// This class extends the `LaunchPadMethodResult` to provide additional functionality for working with parsed command-line arguments.
    /// It allows checking for the presence of specific arguments and retrieving their values with type safety and default value support.
    /// </remarks>
    public sealed class CommandArgsParseResult : LaunchPadMethodResult<CommandArgsParseResultValue>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandArgsParseResult"/> class with the specified result.
        /// </summary>
        /// <param name="result">The parsed result containing the command-line argument values.</param>
        public CommandArgsParseResult(Result<CommandArgsParseResultValue> result) : base(result)
        {
        }

        //internal CliParseResult(Dictionary<string, object?> values)
        //    => _values = new(values, StringComparer.OrdinalIgnoreCase);

        public bool Has(string name)
        {
            return UnderlyingResult.Value.ContainsKey(name);
        }

        public FluentResults.Result<T> Get<T>(string name)
        {
            return UnderlyingResult.Value.Get<T>(name);
        }

        public FluentResults.Result<T> GetOrDefault<T>(string name, T defaultValue = default!)
        {
            return Has(name) ? Get<T>(name) : FluentResults.Result.Ok(defaultValue);
        }
    }

}
