using Castle.Core.Logging;
using Deploy.LaunchPad.Code.Methods;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Tokens;
using FluentResults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Deploy.LaunchPad.Code.CommandLine
{
    /// <summary>
    /// Provides functionality to parse and validate command-line arguments for a given command.
    /// </summary>
    /// <remarks>
    /// This static class is responsible for parsing command-line arguments provided as either a dictionary or a string array.
    /// It validates the arguments against the command's options, converts them to their expected types, and handles default values for optional arguments.
    /// The class also supports error handling for unknown options, missing required options, and invalid values.
    /// </remarks>
    public static partial class CommandArgsParser
    {
        /// <summary>
        /// Parses the provided arguments for a specific command and returns the result.
        /// </summary>
        /// <param name="command">The command definition containing the options and their metadata.</param>
        /// <param name="args">The arguments to parse, which can be a dictionary or a string array.</param>
        /// <returns>
        /// A <see cref="LaunchPadMethodResult{CommandArgsParseResultValue}"/> containing the parsed arguments and their values.
        /// If parsing fails, the result will include error details.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="command"/> or <paramref name="args"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// This method supports two types of input:
        /// <list type="bullet">
        /// <item><description>A dictionary of argument names and values.</description></item>
        /// <item><description>A string array of command-line arguments.</description></item>
        /// </list>
        /// It validates the arguments against the command's options, converts them to their expected types, and fills in default values for optional arguments.
        /// </remarks>
        public static LaunchPadMethodResult<CommandArgsParseResultValue> Parse(ILogger logger, ICommand command, object args)
        {
            Guard.Against<ArgumentNullException>(command == null, "CliParseResult.Parse() => command cannot be null.");
            Guard.Against<ArgumentNullException>(command.Options == null, "CliParseResult.Parse() => command.Options cannot be null.");

            // Create a placeholder dictionary for the CliParseResultValue before we parse the args
            var placeholderValues = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            var validateArgumentsResultValue = new CommandArgsParseResultValue(placeholderValues);
            var validateArgumentsResult = Result.Ok(validateArgumentsResultValue);

            try
            {
                // Check if the arguments are a Dictionary<string, object>
                if (args is Dictionary<string, object> dictionaryArgs)
                {
                    logger.Debug("CommandArgsParser.Parse() => Dictionary<string, object>");
                    // Delegate to ValidateArguments for Dictionary<string, object>
                    return ValidateArguments(command, dictionaryArgs);
                }

                // Check if the arguments are a string[] (existing behavior)
                if (args is string[] argv)
                {
                    var defs = command.Options.ToDictionary(o => o.Name, StringComparer.OrdinalIgnoreCase);
                    var byLong = command.Options.ToDictionary(o => o.LongSwitch, StringComparer.OrdinalIgnoreCase);
                    var byShort = new Dictionary<string, OptionDefinition>(StringComparer.OrdinalIgnoreCase);
                    foreach (var option in command.Options.Where(o => o.ShortSwitch is not null))
                    {
                        byShort.TryAdd(option.ShortSwitch!, option);
                    }
                    var values = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

                    // Cursor-based scan
                    for (int i = 0; i < argv.Length; i++)
                    {
                        var token = argv[i];
                        
                        // --foo=bar
                        if (token.StartsWith("--", StringComparison.Ordinal))
                        {
                            logger.Debug("CommandArgsParser.Parse() => --foo=bar");
                            string key, maybeVal = "";
                            var eqIdx = token.IndexOf('=', StringComparison.Ordinal);
                            if (eqIdx >= 0)
                            {
                                key = token[..eqIdx];
                                maybeVal = token[(eqIdx + 1)..];
                            }
                            else key = token;

                            if (!byLong.TryGetValue(key, out var def))
                            {
                                validateArgumentsResult.WithError($"Unknown option '{key}'.");
                                continue;
                            }

                            if (def.Arity == OptionArity.None)
                            {
                                // Boolean flag; allow explicit "=false"
                                bool val = true;
                                if (eqIdx >= 0 && bool.TryParse(maybeVal, out var b)) val = b;
                                values[def.Name] = val;
                            }
                            else
                            {
                                object? val;
                                object? converted = null; // Ensure converted is always initialized
                                if (eqIdx >= 0)
                                {
                                    val = maybeVal;
                                }
                                else
                                {
                                    if (i + 1 >= argv.Length)
                                    {
                                        validateArgumentsResult.WithError($"Option '{def.LongSwitch}' requires a value.");
                                        continue;
                                    }
                                    val = argv[++i];
                                }

                                // Check if the value is already of the correct type
                                if (val is string strVal)
                                {
                                    if (!TryConvert(strVal, def.ValueType, out converted, out var convErr))
                                    {
                                        validateArgumentsResult.WithError($"Invalid value for '{def.LongSwitch}': {convErr}");
                                        continue;
                                    }
                                }
                                else
                                {
                                    converted = val; // If not a string, assume it's already the correct type
                                }

                                values[def.Name] = converted;
                            }
                            continue;
                        }

                        // -n foo or -n=foo or -y
                        if (token.StartsWith("-", StringComparison.Ordinal))
                        {
                            logger.Debug("CommandArgsParser.Parse() => -n foo or -n=foo or -y");
                            var eqIdx = token.IndexOf('=', StringComparison.Ordinal);
                            var key = eqIdx >= 0 ? token[..eqIdx] : token;

                            if (!byShort.TryGetValue(key, out var def))
                            {
                                validateArgumentsResult.WithError($"Unknown option '{key}'.");
                                continue;
                            }

                            if (def.Arity == OptionArity.None)
                            {
                                bool val = true;
                                if (eqIdx >= 0 && bool.TryParse(token[(eqIdx + 1)..], out var b)) val = b;
                                values[def.Name] = val;
                            }
                            else
                            {
                                object? val;
                                object? converted = null; // Ensure converted is always initialized
                                if (eqIdx >= 0)
                                {
                                    val = token[(eqIdx + 1)..];
                                }
                                else
                                {
                                    if (i + 1 >= argv.Length)
                                    {
                                        validateArgumentsResult.WithError($"Option '{def.ShortSwitch}' requires a value.");
                                        continue;
                                    }
                                    val = argv[++i];
                                }

                                // Check if the value is already of the correct type
                                if (val is string strVal)
                                {
                                    if (!TryConvert(strVal, def.ValueType, out converted, out var convErr))
                                    {
                                        validateArgumentsResult.WithError($"Invalid value for '{def.ShortSwitch}': {convErr}");
                                        continue;
                                    }
                                }
                                else
                                {
                                    converted = val; // If not a string, assume it's already the correct type
                                }

                                values[def.Name] = converted;
                            }
                            continue;
                        }

                        // Positional arguments (if you want them) — not used in the sample.
                        validateArgumentsResult.WithError($"Unexpected argument '{token}'. Use --help for usage.");
                    }

                    // Fill defaults & check requireds
                    foreach (var def in command.Options)
                    {
                        if (!values.ContainsKey(def.Name))
                        {
                            if (def.Required)
                            {
                                string message = $"Missing required option '{def.LongSwitch}'.";
                                logger.Error(message);
                                validateArgumentsResult.WithError(message);
                            }
                            else
                            {
                                // Add default values for optional arguments
                                if (def.Arity == OptionArity.None && def.ValueType == typeof(bool))
                                {
                                    // Flags default to false unless a default was provided
                                    values[def.Name] = def.DefaultValue ?? false;
                                }
                                else if (def.DefaultValue is not null)
                                {
                                    values[def.Name] = def.DefaultValue;
                                }

                                // Add a success message for the default value
                                if (values.ContainsKey(def.Name))
                                {
                                    validateArgumentsResult.WithSuccess($"Default value for '{def.Name}' set to '{values[def.Name]}'.");
                                }
                            }
                        }
                    }

                    // Return a success result for each parsed result
                    foreach (var value in values)
                    {
                        validateArgumentsResult.Value.Values.TryAdd(value.Key, value.Value);
                        validateArgumentsResult.WithSuccess($"Parsed option '{value.Key}' with value '{value.Value}'.");
                    }
                }
                else
                {
                    // If args is neither a Dictionary nor a string array, throw an error
                    validateArgumentsResult.WithError("Unsupported argument type. Expected Dictionary<string, object> or string[].");
                }
            }
            catch (Exception ex)
            {
                // Catch any unexpected exceptions and return a failure result
                validateArgumentsResult.WithError($"An unexpected error occurred: {ex.Message}");
            }

            // Return the result
            return new LaunchPadMethodResult<CommandArgsParseResultValue>(validateArgumentsResult);
        }

        /// <summary>
        /// Validates arguments being passed in to a command against basic constraints of type and optionality.
        /// </summary>
        /// <param name="command">The command to parse arguments for.</param>
        /// <param name="args">The dictionary of arguments to parse.</param>
        /// <returns>A parsed CliParseResultValue.</returns>
        /// <exception cref="Exception">Throws an exception if there are parsing errors.</exception>
        private static LaunchPadMethodResult<CommandArgsParseResultValue> ValidateArguments(ICommand command, Dictionary<string, object> args)
        {
            Guard.Against<ArgumentNullException>(command == null, "CliParseResult.ValidateArguments() => command cannot be null.");
            Guard.Against<ArgumentNullException>(command.Options == null, "CliParseResult.ValidateArguments() => command.Options cannot be null.");
            Guard.Against<ArgumentNullException>(args == null, "CliParseResult.ValidateArguments() => args cannot be null.");

            // Create a placeholder dictionary for the CliParseResultValue before we validate the args
            var placeholderValues = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            var validateArgumentsResultValue = new CommandArgsParseResultValue(placeholderValues);
            var validateArgumentsResult = Result.Ok(validateArgumentsResultValue);

            try
            {
                // Convert arguments to a string array for CliParser.Parse
                var argv = args.SelectMany(kvp =>
                {
                    if (kvp.Value is Dictionary<string, object> nestedDictionaryWithObject)
                    {
                        var serializedDictionary = System.Text.Json.JsonSerializer.Serialize(nestedDictionaryWithObject);
                        return new[] { $"--{kvp.Key}", serializedDictionary };
                    }
                    if (kvp.Value is Dictionary<string, string> nestedDictionaryWithString)
                    {
                        var serializedDictionary = System.Text.Json.JsonSerializer.Serialize(nestedDictionaryWithString);
                        return new[] { $"--{kvp.Key}", serializedDictionary };
                    }
                    else if (kvp.Value is Dictionary<string, LaunchPadToken> tokenDictionary)
                    {
                        var serializedDictionary = System.Text.Json.JsonSerializer.Serialize(tokenDictionary);
                        return new[] { $"--{kvp.Key}", serializedDictionary };
                    }
                    else if (kvp.Value is IElementNameLight elementNameValue)
                    {
                        return new[] { $"--{kvp.Key}", elementNameValue.Full };
                    }
                    else if (kvp.Value is string stringValue || kvp.Value is int || kvp.Value is uint || kvp.Value is long || kvp.Value is double || kvp.Value is float || kvp.Value is decimal)
                    {
                        return new[] { $"--{kvp.Key}", kvp.Value.ToString() };
                    }
                    else
                    {
                        return new[] { $"--{kvp.Key}", kvp.Value?.ToString() ?? string.Empty };
                    }
                }).ToArray();

                // Parse the arguments
                var parseResult = ParseFromStringArray(command, argv, out var error);

                if (error != null)
                {
                    // Return a failure result with the error message
                    validateArgumentsResult.WithError(error);
                }

                // Add parsed arguments to validateArgumentsResult.Value.Values
                foreach (var parsedValue in parseResult.UnderlyingResult.Value.Values)
                {
                    validateArgumentsResult.Value.Values.TryAdd(parsedValue.Key, parsedValue.Value);
                }

                // Fill defaults & check requireds
                foreach (var def in command.Options)
                {
                    if (!validateArgumentsResult.Value.Values.ContainsKey(def.Name))
                    {
                        if (def.Required)
                        {
                            validateArgumentsResult.WithError($"Missing required option '{def.LongSwitch}'.");
                        }
                        else
                        {
                            // Add default values for optional arguments
                            if (def.Arity == OptionArity.None && def.ValueType == typeof(bool))
                            {
                                // Flags default to false unless a default was provided
                                validateArgumentsResult.Value.Values.TryAdd(def.Name, def.DefaultValue ?? false);
                            }
                            else if (def.DefaultValue is not null)
                            {
                                validateArgumentsResult.Value.Values.TryAdd(def.Name, def.DefaultValue);
                            }

                            // Add a success message for the default value
                            if (validateArgumentsResult.Value.Values.ContainsKey(def.Name))
                            {
                                validateArgumentsResult.WithSuccess($"Default value for '{def.Name}' set to '{validateArgumentsResult.Value.Values[def.Name]}'.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch any unexpected exceptions and return a failure result
                validateArgumentsResult.WithError($"An unexpected error occurred: {ex.Message}");
            }

            var methodOutput = (LaunchPadMethodResult<CommandArgsParseResultValue>)(object)new CommandArgsParseResult(validateArgumentsResult); // Cast to LaunchPadMethodResult<TValue>
            return methodOutput;
        }

        /// <summary>
        /// Parses a string array of command-line arguments into a structured <see cref="CommandArgsParseResult"/>.
        /// </summary>
        /// <param name="command">The command definition containing the options and their metadata.</param>
        /// <param name="argv">The array of command-line arguments to parse.</param>
        /// <param name="error">
        /// An output parameter that will contain an error message if parsing fails.
        /// If parsing succeeds, this will be set to <c>null</c>.
        /// </param>
        /// <returns>
        /// A <see cref="CommandArgsParseResult"/> containing the parsed arguments and their values.
        /// If parsing fails, the result will contain the error details.
        /// </returns>
        /// <remarks>
        /// This method performs the following tasks:
        /// <list type="bullet">
        /// <item><description>Validates the arguments against the command's options.</description></item>
        /// <item><description>Handles both long-form options (e.g., <c>--option=value</c>) and short-form options (e.g., <c>-o value</c>).</description></item>
        /// <item><description>Converts argument values to their expected types (e.g., <c>int</c>, <c>bool</c>, <c>FileInfo</c>).</description></item>
        /// <item><description>Fills in default values for options that are not provided.</description></item>
        /// <item><description>Returns errors for unknown options, missing required options, or invalid values.</description></item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="command"/> or <paramref name="argv"/> is <c>null</c>.
        /// </exception>
        private static CommandArgsParseResult ParseFromStringArray(ICommand command, string[] argv, out string? error)
        {
            Guard.Against<ArgumentNullException>(command == null, "CliParseResult.ValidateArguments() => command cannot be null.");
            Guard.Against<ArgumentNullException>(command.Options == null, "CliParseResult.ValidateArguments() => command.Options cannot be null.");
            Guard.Against<ArgumentNullException>(argv == null, "CliParseResult.ValidateArguments() => argv cannot be null.");

            error = null;

            var defs = command.Options.ToDictionary(o => o.Name, StringComparer.OrdinalIgnoreCase);
            var byLong = command.Options.ToDictionary(o => o.LongSwitch, StringComparer.OrdinalIgnoreCase);
            var byShort = new Dictionary<string, OptionDefinition>(StringComparer.OrdinalIgnoreCase);
            foreach (var option in command.Options.Where(o => o.ShortSwitch is not null))
            {
                byShort.TryAdd(option.ShortSwitch!, option);
            }
            var values = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

            // Cursor-based scan
            for (int i = 0; i < argv.Length; i++)
            {
                var token = argv[i];

                // --foo=bar
                if (token.StartsWith("--", StringComparison.Ordinal))
                {
                    string key, maybeVal = "";
                    var eqIdx = token.IndexOf('=', StringComparison.Ordinal);
                    if (eqIdx >= 0)
                    {
                        key = token[..eqIdx];
                        maybeVal = token[(eqIdx + 1)..];
                    }
                    else key = token;

                    if (!byLong.TryGetValue(key, out var def))
                    {
                        error = $"Unknown option '{key}'.";
                        return new CommandArgsParseResult(Result.Fail<CommandArgsParseResultValue>(error));
                    }

                    if (def.Arity == OptionArity.None)
                    {
                        // Boolean flag; allow explicit "=false"
                        bool val = true;
                        if (eqIdx >= 0 && bool.TryParse(maybeVal, out var b)) val = b;
                        values[def.Name] = val;
                    }
                    else
                    {
                        object? val;
                        object? converted = null; // Ensure converted is always initialized
                        if (eqIdx >= 0)
                        {
                            val = maybeVal;
                        }
                        else
                        {
                            if (i + 1 >= argv.Length)
                            {
                                error = $"Option '{def.LongSwitch}' requires a value.";
                                return new CommandArgsParseResult(Result.Fail<CommandArgsParseResultValue>(error));
                            }
                            val = argv[++i];
                        }

                        // Check if the value is already of the correct type
                        if (val is string strVal)
                        {
                            if (!TryConvert(strVal, def.ValueType, out converted, out var convErr))
                            {
                                error = $"Invalid value for '{def.LongSwitch}': {convErr}";
                                return new CommandArgsParseResult(Result.Fail<CommandArgsParseResultValue>(error));
                            }
                        }
                        else
                        {
                            converted = val; // If not a string, assume it's already the correct type
                        }

                        values[def.Name] = converted;
                    }
                    continue;
                }

                // -n foo or -n=foo or -y
                if (token.StartsWith("-", StringComparison.Ordinal))
                {
                    var eqIdx = token.IndexOf('=', StringComparison.Ordinal);
                    var key = eqIdx >= 0 ? token[..eqIdx] : token;

                    if (!byShort.TryGetValue(key, out var def))
                    {
                        error = $"Unknown option '{key}'.";
                        return new CommandArgsParseResult(Result.Fail<CommandArgsParseResultValue>(error));
                    }

                    if (def.Arity == OptionArity.None)
                    {
                        bool val = true;
                        if (eqIdx >= 0 && bool.TryParse(token[(eqIdx + 1)..], out var b)) val = b;
                        values[def.Name] = val;
                    }
                    else
                    {
                        object? val;
                        object? converted = null; // Ensure converted is always initialized
                        if (eqIdx >= 0)
                        {
                            val = token[(eqIdx + 1)..];
                        }
                        else
                        {
                            if (i + 1 >= argv.Length)
                            {
                                error = $"Option '{def.ShortSwitch}' requires a value.";
                                return new CommandArgsParseResult(Result.Fail<CommandArgsParseResultValue>(error));
                            }
                            val = argv[++i];
                        }

                        // Check if the value is already of the correct type
                        if (val is string strVal)
                        {
                            if (!TryConvert(strVal, def.ValueType, out converted, out var convErr))
                            {
                                error = $"Invalid value for '{def.ShortSwitch}': {convErr}";
                                return new CommandArgsParseResult(Result.Fail<CommandArgsParseResultValue>(error));
                            }
                        }
                        else
                        {
                            converted = val; // If not a string, assume it's already the correct type
                        }

                        values[def.Name] = converted;
                    }
                    continue;
                }

                // Positional arguments (if you want them) — not used in the sample.
                error = $"Unexpected argument '{token}'. Use --help for usage.";
                return new CommandArgsParseResult(Result.Fail<CommandArgsParseResultValue>(error));
            }

            // Fill defaults & check requireds
            foreach (var def in command.Options)
            {
                if (!values.ContainsKey(def.Name))
                {
                    if (def.Required)
                    {
                        error = $"Missing required option '{def.LongSwitch}'.";
                        return new CommandArgsParseResult(Result.Fail<CommandArgsParseResultValue>(error));
                    }
                    if (def.Arity == OptionArity.None && def.ValueType == typeof(bool))
                    {
                        // Flags default to false unless a default was provided
                        values[def.Name] = def.DefaultValue ?? false;
                    }
                    else if (def.DefaultValue is not null)
                    {
                        values[def.Name] = def.DefaultValue;
                    }
                }
            }
            // Wrap the values dictionary in a CliParseResultValue and return it
            return new CommandArgsParseResult(Result.Ok(new CommandArgsParseResultValue(values)));
        }

        private static bool TryConvert(string? input, Type target, out object? value, out string? error)
        {
            error = null;
            value = null;

            if (target == typeof(string))
            {
                value = input ?? "";
                return true;
            }
            if (target == typeof(bool))
            {
                if (bool.TryParse(input, out var b)) { value = b; return true; }
                error = "expected 'true' or 'false'";
                return false;
            }
            if (target == typeof(int))
            {
                if (int.TryParse(input, out var n)) { value = n; return true; }
                error = "expected an integer";
                return false;
            }
            if (target == typeof(long))
            {
                if (long.TryParse(input, out var l)) { value = l; return true; }
                error = "expected a long integer";
                return false;
            }
            if (target == typeof(double))
            {
                if (double.TryParse(input, out var d)) { value = d; return true; }
                error = "expected a double (floating-point number)";
                return false;
            }
            if (target == typeof(float))
            {
                if (float.TryParse(input, out var f)) { value = f; return true; }
                error = "expected a float (single-precision number)";
                return false;
            }
            if (target == typeof(decimal))
            {
                if (decimal.TryParse(input, out var dec)) { value = dec; return true; }
                error = "expected a decimal (high-precision number)";
                return false;
            }
            if (target == typeof(Guid))
            {
                if (Guid.TryParse(input, out var g)) { value = g; return true; }
                error = "expected a valid GUID";
                return false;
            }
            if (target == typeof(DateTime))
            {
                if (DateTime.TryParse(input, out var dt)) { value = dt; return true; }
                error = "expected a valid DateTime";
                return false;
            }
            if (target == typeof(DateOnly))
            {
                if (DateOnly.TryParse(input, out var date)) { value = date; return true; }
                error = "expected a valid DateOnly";
                return false;
            }
            if (target == typeof(TimeOnly))
            {
                if (TimeOnly.TryParse(input, out var time)) { value = time; return true; }
                error = "expected a valid TimeOnly";
                return false;
            }
            if (target == typeof(TimeSpan))
            {
                if (TimeSpan.TryParse(input, out var ts)) { value = ts; return true; }
                error = "expected a valid TimeSpan (e.g., 'hh:mm:ss')";
                return false;
            }
            if (target == typeof(Uri))
            {
                if (Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out var uri)) { value = uri; return true; }
                error = "expected a valid URI";
                return false;
            }
            if (target == typeof(System.Net.IPAddress))
            {
                if (System.Net.IPAddress.TryParse(input, out var ip)) { value = ip; return true; }
                error = "expected a valid IP address";
                return false;
            }
            if (target == typeof(Version))
            {
                if (Version.TryParse(input, out var version)) { value = version; return true; }
                error = "expected a valid version (e.g., '1.0.0')";
                return false;
            }
            if (target == typeof(FileInfo))
            {
                if (string.IsNullOrWhiteSpace(input)) { error = "expected a file path"; return false; }
                value = new FileInfo(input);
                return true;
            }
            if (target == typeof(DirectoryInfo))
            {
                if (string.IsNullOrWhiteSpace(input)) { error = "expected a directory path"; return false; }
                value = new DirectoryInfo(input);
                return true;
            }
            if (target == typeof(System.Numerics.BigInteger))
            {
                if (System.Numerics.BigInteger.TryParse(input, out var bigInt)) { value = bigInt; return true; }
                error = "expected a valid BigInteger";
                return false;
            }
            // Add support for enums
            if (target.IsEnum)
            {
                if (Enum.TryParse(target, input, ignoreCase: true, out var enumValue))
                {
                    value = enumValue;
                    return true;
                }
                error = $"expected one of: {string.Join(", ", Enum.GetNames(target))}";
                return false;
            }
            // Add support for Dictionary<string, LaunchPadToken>
            if (target == typeof(Dictionary<string, LaunchPadToken>))
            {
                try
                {
                    // Assume the input is already a serialized dictionary
                    value = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, LaunchPadToken>>(input!);
                    return true;
                }
                catch (Exception ex)
                {
                    error = $"failed to parse Dictionary<string, LaunchPadToken>: {ex.Message}";
                    return false;
                }
            }
            // Extend with more primitives as needed
            error = $"unsupported type {target.Name}";
            return false;
        }
    }

}
