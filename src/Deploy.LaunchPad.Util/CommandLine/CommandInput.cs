using Castle.Core.Logging;
using Deploy.LaunchPad.FactoryLite.Methods;
using Deploy.LaunchPad.Util.CommandLine;
using Deploy.LaunchPad.Util.Methods;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Deploy.LaunchPad.FactoryLite.CommandLine
{
    public partial record CommandInput : LaunchPadMethodInputBase
    {
        private readonly Dictionary<string, object> _args;

        /// <summary>
        /// Holds the raw arguments for a command.
        /// </summary>
        public virtual Dictionary<string, object> RawArgs => _args;


        /// <summary>
        /// Holds the parsed (already converted from raw) values that will be used for a command.
        /// </summary>
        public virtual CommandArgsParseResultValue Args { get; init; }

        /// <summary>
        /// Provides access to the service provider for resolving dependencies.
        /// </summary>
        public virtual IServiceProvider Services { get; init; }

        /// <summary>
        /// Represents the cancellation token for the command, allowing cancellation of a long-running operation.
        /// </summary>
        public virtual CancellationToken Ct { get; init; }

        public virtual ExceptionHandlingStrategy ExceptionHandling { get; init; } = ExceptionHandlingStrategy.ReturnResultWithError;

        public delegate LaunchPadMethodResult<TResultValue> ExceptionHandler<TResultValue>(Exception ex) where TResultValue : class, ILaunchPadMethodResultValue;

        /// <summary>
        /// A delegate for custom exception handling logic.
        /// If provided, this will be invoked when an exception occurs.
        /// </summary>
        public virtual ExceptionHandler<LaunchPadMethodResultValueBase>? CustomExceptionHandler { get; init; }

        // Client-agnostic properties

        /// <summary>
        /// Represents the user, ai, or system initiating the command.
        /// Useful for tracking and auditing purposes.
        /// </summary>
        public virtual JObject? UserContext { get; init; }

        /// <summary>
        /// Represents information about the environment in which the command is being executed or targeted. 
        /// Use it to specify environment-sensitive information or settings to consider such as:
        /// in which environment the command is being executed (e.g., Development, Staging, Production);
        /// the target environment for the command; etc.
        /// </summary>
        public virtual JObject? EnvironmentContext { get; init; }

        //// <summary>
        /// Represents information about the client initiating the command.
        /// Useful for storing client-specific metadata, such as client type, version, IP address, or platform.
        /// </summary>
        public virtual JObject? ClientContext { get; init; }

        /// <summary>
        /// A dictionary for storing additional metadata (in JSON format) that may vary between clients.
        /// Provides flexibility for client-specific data without modifying the class structure.
        /// </summary>
        public virtual IDictionary<string, JToken> CustomMetadata { get; init; } = new Dictionary<string, JToken>();

        /// <summary>
        /// Represents the timestamp when the command was created or initiated.
        /// Defaults to the current UTC time.
        /// </summary>
        public virtual DateTime Timestamp { get; init; } = DateTime.UtcNow;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandInput"/> class.
        /// </summary>
        public CommandInput() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandInput"/> class with raw arguments.
        /// </summary>
        /// <param name="logger">The logger to use for logging operations.</param>
        /// <param name="rawArgs">The raw arguments for the command.</param>
        /// <param name="services">The service provider for resolving dependencies.</param>
        /// <param name="ct">The cancellation token for the command.</param>
        public CommandInput(ILogger logger, Dictionary<string, object> rawArgs, IServiceProvider services, CancellationToken ct, ICommand command) : base(logger)
        {
            _args = rawArgs;
            Services = services;
            Ct = ct;

            // Parse the raw arguments into CliParseResultValue
            Args = ParseArgs(logger, command);
        }

        /// <summary>
        /// Parses the raw arguments into a CliParseResultValue.
        /// </summary>
        private Deploy.LaunchPad.Util.CommandLine.CommandArgsParseResultValue ParseArgs(ILogger logger, ICommand command)
        {
            // Use the CliParser to parse the raw arguments
            var parseResult = Deploy.LaunchPad.Util.CommandLine.CommandArgsParser.Parse(
                logger,
                command,
                _args.Select(kvp => new[] { $"--{kvp.Key}", kvp.Value.ToString() }).SelectMany(x => x).ToArray()
            );

            if (parseResult == null || !parseResult.Succeeded)
            {
                throw new InvalidOperationException($"Failed to parse arguments: {parseResult?.Errors}");
            }

            return parseResult.UnderlyingResult.Value;
        }

        /// <summary>
        /// Creates a new CommandInput based on the current instance, with optional overrides for specific properties.
        /// </summary>
        /// <param name="command">The ICommand instance for parsing the arguments.</param>
        /// <param name="rawArgs">The raw arguments for the new CommandInput.</param>
        /// <returns>A new CommandInput instance.</returns>
        public static CommandInput CreateInputForNestedCommand(CommandInput parentInput, ICommand nestedCommand, Dictionary<string, object> nestedRawArgs)
        {
            return new CommandInput(
                parentInput.Logger,
                nestedRawArgs,
                parentInput.Services,
                parentInput.Ct,
                nestedCommand
            );
        }

        /// <summary>
        /// Retrieves and converts an option value from the CommandInput.Args dictionary.
        /// </summary>
        /// <typeparam name="T">The expected type of the option value.</typeparam>
        /// <param name="input">The CommandInput object.</param>
        /// <param name="optionName">The name of the option to retrieve.</param>
        /// <returns>The converted value of the option.</returns>
        public virtual T GetOptionValue<T>(string optionName)
        {
            // Ensure the Args dictionary is not null
            if (Args == null)
            {
                throw new ArgumentNullException(nameof(Args), "CommandInput.Args cannot be null.");
            }

            // Retrieve the raw value from the Args dictionary
            var rawValue = Args.Get<string>(optionName).ValueOrDefault;

            // If the expected type is a string, return the raw value directly
            if (typeof(T) == typeof(string))
            {
                return (T)(object)rawValue;
            }

            // Attempt to deserialize the raw value into the expected type
            try
            {
                return JsonSerializer.Deserialize<T>(rawValue, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (JsonException ex)
            {
                throw new ArgumentException($"Invalid value for '{optionName}': {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves and converts an option value from the CommandInput.Args dictionary as a list.
        /// </summary>
        /// <typeparam name="T">The expected type of the list elements.</typeparam>
        /// <param name="input">The CommandInput object.</param>
        /// <param name="optionName">The name of the option to retrieve.</param>
        /// <returns>The converted list of values.</returns>
        /// <summary>
        /// Retrieves and converts an option value from the CommandInput.Args dictionary as a list.
        /// </summary>
        /// <typeparam name="T">The expected type of the list elements.</typeparam>
        /// <param name="input">The CommandInput object.</param>
        /// <param name="optionName">The name of the option to retrieve.</param>
        /// <returns>The converted list of values.</returns>
        public virtual List<T> GetOptionsListValue<T>(string optionName)
        {
            // Ensure the Args dictionary is not null
            if (Args == null)
            {
                throw new ArgumentNullException(nameof(Args), "CommandInput.Args cannot be null.");
            }

            // Retrieve the raw value from the Args dictionary
            var rawValue = Args.Get<string>(optionName).ValueOrDefault;

            // Log the raw JSON value for debugging purposes
            Logger?.Debug($"Raw JSON value for '{optionName}': {rawValue}");

            // Attempt to deserialize the raw value into a list of the expected type
            try
            {
                return JsonSerializer.Deserialize<List<T>>(rawValue, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                        ?? new List<T>(); // Return an empty list if deserialization results in null
            }
            catch (JsonException ex)
            {
                Logger?.Error($"Failed to deserialize JSON for '{optionName}'. Error: {ex.Message}");
                throw new ArgumentException($"Invalid value for '{optionName}': {ex.Message}");
            }
        }
    }
}