using Deploy.LaunchPad.Code.Methods;
using FluentResults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.CommandLine
{
    /// <summary>
    /// Represents the result of parsing command-line arguments, encapsulating the parsed values and providing utility methods for retrieval.
    /// </summary>
    /// <remarks>
    /// This class is designed to store and manage the parsed arguments from a command-line input. 
    /// It provides methods to retrieve argument values with type safety and error handling.
    /// </remarks>
    public sealed partial class CommandArgsParseResultValue : LaunchPadMethodResultValueBase, ILaunchPadMethodResultValue
    {

        private readonly Dictionary<string, object?> _values;

        public Dictionary<string, object?> Values => _values;


        internal CommandArgsParseResultValue(Dictionary<string, object?> values)
            => _values = new(values, StringComparer.OrdinalIgnoreCase);

        public bool ContainsKey(string name)
        {
            return _values.ContainsKey(name);
        }

        /// <summary>
        /// Asynchronously checks if the specified key exists in the parsed arguments.
        /// </summary>
        public async Task<bool> ContainsKeyAsync(string name)
        {
            // Simulate asynchronous behavior (e.g., if future logic involves async operations)
            await Task.Yield();

            // Reuse the synchronous ContainsKey logic
            return ContainsKey(name);
        }

        /// <summary>
        /// Retrieves the value associated with the specified key, or returns an error if the key is missing or the type is invalid.
        /// </summary>
        public FluentResults.Result<T> Get<T>(string name)
        {
            if (!_values.TryGetValue(name, out var val))
            {
                // Return a result with an error for a missing key
                return FluentResults.Result.Fail<T>($"Missing option '{name}'.");
            }

            if (val is null)
            {
                // Return a result with the default value for null
                return FluentResults.Result.Ok(default(T)!);
            }

            if (val is T ok)
            {
                // Return a successful result with the value
                return FluentResults.Result.Ok(ok);
            }

            // Handle FileInfo conversion convenience
            if (typeof(T) == typeof(string) && val is FileInfo fi)
            {
                return FluentResults.Result.Ok((T)(object)fi.FullName);
            }

            // Return a result with an error for invalid type casting
            return FluentResults.Result.Fail<T>($"Option '{name}' is of type {val.GetType().Name}, not {typeof(T).Name}.");
        }

        /// <summary>
        /// Asynchronously retrieves the value associated with the specified key, or returns an error if the key is missing or the type is invalid.
        /// </summary>
        public async Task<FluentResults.Result<T>> GetAsync<T>(string name)
        {
            // Simulate asynchronous behavior (e.g., if future logic involves async operations)
            await Task.Yield();

            // Reuse the synchronous Get<T> logic
            return Get<T>(name);
        }

        /// <summary>
        /// Retrieves the value associated with the specified key, or returns the default value if the key is missing.
        /// </summary>
        public FluentResults.Result<T> GetOrDefault<T>(string name, T defaultValue = default!)
        {
            return _values.ContainsKey(name) ? Get<T>(name) : FluentResults.Result.Ok(defaultValue);
        }

        /// <summary>
        /// Asynchronously retrieves the value associated with the specified key, or returns the default value if the key is missing.
        /// </summary>
        public async Task<FluentResults.Result<T>> GetOrDefaultAsync<T>(string name, T defaultValue = default!)
        {
            // Simulate asynchronous behavior (e.g., if future logic involves async operations)
            await Task.Yield();

            // Reuse the synchronous GetOrDefault<T> logic
            return _values.ContainsKey(name) ? await GetAsync<T>(name) : FluentResults.Result.Ok(defaultValue);
        }
    }
}
