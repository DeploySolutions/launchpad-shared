using Castle.Core.Logging;
using Deploy.LaunchPad.FactoryLite.CommandLine;
using Deploy.LaunchPad.FactoryLite.Methods;
using Deploy.LaunchPad.Util.Methods;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{
   

    public partial class CommandHelper : HelperBase
    {
        public CommandArgValueSeparator ArgumentSeparator { get; set; }
       

        public CommandHelper() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CommandHelper(ILogger logger) : base(logger)
        {

        }

        /// <summary>
        /// Converts command-line arguments into a Dictionary of key value pairs. Cmd arguments must be in form key[separator character]value.
        /// </summary>
        /// <param name="args">The string array containing the key value pairs</param>
        /// <returns>A dictionary containing the key value pairs</returns>
        public virtual Dictionary<string, string> ResolveArguments(string[] args, CommandArgValueSeparator separator)
        {
            // determine how the user is separating the argument name and value
            var charSeparator = new char();
            switch (separator)
            {
                case CommandArgValueSeparator.Colon:
                    charSeparator = ':';
                    break;
                case CommandArgValueSeparator.Equals:
                    charSeparator = '=';
                    break;
                case CommandArgValueSeparator.SingleSpace:
                    charSeparator = ' ';
                    break;
            }

            if (args == null)
                return null;

            if (args.Length >= 1)
            {
                var arguments = new Dictionary<string, string>();
                foreach (string argument in args)
                {
                    int idx = argument.IndexOf(charSeparator);
                    if (idx > 0)
                        arguments[argument.Substring(0, idx)] = argument.Substring(idx + 1);
                }
                return arguments;
            }

            return null;
        }

        /// <summary>
        /// Retrieves a boolean value from the command arguments.
        /// If the argument is not present or cannot be parsed, the default value is returned.
        /// </summary>
        /// <param name="input">The command input containing the arguments.</param>
        /// <param name="key">The key of the argument to retrieve.</param>
        /// <param name="defaultValue">The default value to return if the argument is not present or invalid.</param>
        /// <returns>The boolean value of the argument, or the default value if not found or invalid.</returns>
        /// <example>
        /// Args format:
        /// {
        ///   "shouldMoveSource": true
        /// }
        /// </example>
        public virtual bool GetBoolValueFromArgs(CommandInput input, string key, bool defaultValue)
        {
            Guard.Against<ArgumentNullException>(input == null, "CommandBase.GetBoolValueFromArgs() => input cannot be null.");
            Guard.Against<ArgumentNullException>(input.Args == null, "CommandBase.GetBoolValueFromArgs() => input.Args cannot be null.");
            if (input.Args.ContainsKey(key))
            {
                try
                {
                    var value = input.Args.GetOrDefault<object>(key, null).ValueOrDefault;
                    if (value is bool boolValue)
                    {
                        return boolValue;
                    }
                    if (value is string stringValue)
                    {
                        // If the value is null, it means the flag was passed without a value, so default to true
                        if (string.IsNullOrEmpty(stringValue))
                        {
                            return true;
                        }
                        // Otherwise, parse the string value as a boolean
                        if (bool.TryParse(stringValue, out var result))
                        {
                            return result;
                        }
                        Logger.Warn($"Invalid boolean value for argument '{key}': {stringValue}. Using default: {defaultValue}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error retrieving argument '{key}': {ex.Message}. Using default: {defaultValue}");
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Retrieves a string value from the command arguments.
        /// If the argument is not present, the default value is returned.
        /// </summary>
        /// <param name="input">The command input containing the arguments.</param>
        /// <param name="key">The key of the argument to retrieve.</param>
        /// <param name="defaultValue">The default value to return if the argument is not present.</param>
        /// <returns>The string value of the argument, or the default value if not found.</returns>
        /// <example>
        /// Args format:
        /// {
        ///   "source": "C:\\SourceFolder"
        /// }
        /// </example>
        public virtual string GetStringValueFromArgs(CommandInput input, string key, string defaultValue)
        {
            Guard.Against<ArgumentNullException>(input == null, "CommandBase.GetBoolValueFromArgs() => input cannot be null.");
            Guard.Against<ArgumentNullException>(input.Args == null, "CommandBase.GetBoolValueFromArgs() => input.Args cannot be null.");
            if (input.Args.ContainsKey(key))
            {
                return input.Args.Get<string>(key).ValueOrDefault;
            }

            // Fallback to the default value
            return defaultValue;
        }

        /// <summary>
        /// Retrieves a string value from the command arguments.
        /// If the argument is not present, the default value is returned.
        /// </summary>
        /// <param name="input">The command input containing the arguments.</param>
        /// <param name="key">The key of the argument to retrieve.</param>
        /// <param name="defaultValue">The default value to return if the argument is not present.</param>
        /// <returns>The string value of the argument, or the default value if not found.</returns>
        /// <example>
        /// Args format:
        /// {
        ///   "source": "C:\\SourceFolder"
        /// }
        /// </example>
        public virtual int GetIntValueFromArgs(CommandInput input, string key, int defaultValue)
        {
            Guard.Against<ArgumentNullException>(input == null, "CommandBase.GetBoolValueFromArgs() => input cannot be null.");
            Guard.Against<ArgumentNullException>(input.Args == null, "CommandBase.GetBoolValueFromArgs() => input.Args cannot be null.");
            if (input.Args.ContainsKey(key))
            {
                try
                {
                    var value = input.Args.GetOrDefault<object>(key, null).ValueOrDefault;
                    if (value is int intValue)
                    {
                        return intValue;
                    }
                    if (value is string stringValue && int.TryParse(stringValue, out var result))
                    {
                        return result;
                    }
                    Logger.Warn($"Invalid integer value for argument '{key}': {value}. Using default: {defaultValue}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error retrieving argument '{key}': {ex.Message}. Using default: {defaultValue}");
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Retrieves a long value from the command arguments.
        /// If the argument is not present or cannot be parsed, the default value is returned.
        /// </summary>
        /// <param name="input">The command input containing the arguments.</param>
        /// <param name="key">The key of the argument to retrieve.</param>
        /// <param name="defaultValue">The default value to return if the argument is not present or invalid.</param>
        /// <returns>The long value of the argument, or the default value if not found or invalid.</returns>
        /// <example>
        /// Args format:
        /// {
        ///   "fileSizeLimit": 1048576
        /// }
        /// </example>
        public virtual long GetLongValueFromArgs(CommandInput input, string key, long defaultValue)
        {
            Guard.Against<ArgumentNullException>(input == null, "CommandBase.GetBoolValueFromArgs() => input cannot be null.");
            Guard.Against<ArgumentNullException>(input.Args == null, "CommandBase.GetBoolValueFromArgs() => input.Args cannot be null.");
            if (input.Args.ContainsKey(key))
            {
                try
                {
                    var value = input.Args.GetOrDefault<object>(key, null).ValueOrDefault;
                    if (value is long longValue)
                    {
                        return longValue;
                    }
                    if (value is string stringValue && long.TryParse(stringValue, out var result))
                    {
                        return result;
                    }
                    Logger.Warn($"Invalid long value for argument '{key}': {value}. Using default: {defaultValue}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error retrieving argument '{key}': {ex.Message}. Using default: {defaultValue}");
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Retrieves a DateTime value from the command arguments.
        /// If the argument is not present or cannot be parsed, the default value is returned.
        /// The DateTimeKind parameter specifies whether the DateTime should be treated as UTC, Local, or Unspecified.
        /// </summary>
        /// <param name="input">The command input containing the arguments.</param>
        /// <param name="key">The key of the argument to retrieve.</param>
        /// <param name="defaultValue">The default value to return if the argument is not present or invalid.</param>
        /// <param name="kind">Specifies the DateTimeKind (UTC, Local, or Unspecified) for the returned DateTime. Defaults to DateTimeKind.Utc.</param>
        /// <returns>The DateTime value of the argument, or the default value if not found or invalid, with the specified DateTimeKind.</returns>
        /// <example>
        /// Args format:
        /// {
        ///   "startDate": "2023-10-01T12:00:00Z"
        /// }
        /// Example usage:
        /// DateTime startDate = GetDateTimeValueFromArgs(input, "startDate", DateTime.UtcNow, DateTimeKind.Utc);
        /// DateTime localDate = GetDateTimeValueFromArgs(input, "startDate", DateTime.Now, DateTimeKind.Local);
        /// </example>
        public virtual DateTime GetDateTimeValueFromArgs(CommandInput input, string key, DateTime defaultValue, DateTimeKind kind = DateTimeKind.Utc)
        {
            Guard.Against<ArgumentNullException>(input == null, "CommandBase.GetDateTimeValueFromArgs() => input cannot be null.");
            Guard.Against<ArgumentNullException>(input.Args == null, "CommandBase.GetDateTimeValueFromArgs() => input.Args cannot be null.");

            if (input.Args.ContainsKey(key))
            {
                try
                {
                    var value = input.Args.GetOrDefault<object>(key, null).ValueOrDefault;
                    if (value is DateTime dateTimeValue)
                    {
                        return DateTime.SpecifyKind(dateTimeValue, kind); // Use specified kind
                    }
                    if (value is string stringValue && DateTime.TryParse(stringValue, out var result))
                    {
                        return DateTime.SpecifyKind(result, kind); // Use specified kind
                    }
                    Logger.Warn($"Invalid DateTime value for argument '{key}': {value}. Using default: {defaultValue}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error retrieving argument '{key}': {ex.Message}. Using default: {defaultValue}");
                }
            }

            return DateTime.SpecifyKind(defaultValue, kind); // Use specified kind for default
        }


        /// <summary>
        /// Retrieves an enum value from the command arguments.
        /// If the argument is not present or cannot be parsed, the default value is returned.
        /// </summary>
        /// <typeparam name="TEnum">The enum type to retrieve.</typeparam>
        /// <param name="input">The command input containing the arguments.</param>
        /// <param name="key">The key of the argument to retrieve.</param>
        /// <param name="defaultValue">The default value to return if the argument is not present or invalid.</param>
        /// <returns>The enum value of the argument, or the default value if not found or invalid.</returns>
        public virtual TEnum GetEnumValueFromArgs<TEnum>(CommandInput input, string key, TEnum defaultValue) where TEnum : struct, Enum
        {
            Guard.Against<ArgumentNullException>(input == null, "CommandHelper.GetEnumValueFromArgs() => input cannot be null.");
            Guard.Against<ArgumentNullException>(input.Args == null, "CommandHelper.GetEnumValueFromArgs() => input.Args cannot be null.");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(key), "CommandHelper.GetEnumValueFromArgs() => key cannot be null or empty.");

            if (input.Args.ContainsKey(key))
            {
                try
                {
                    var value = input.Args.Get<string>(key).ValueOrDefault;
                    if (Enum.TryParse(value, true, out TEnum result))
                    {
                        return result;
                    }
                    Logger.Warn($"Invalid enum value for argument '{key}': {value}. Using default: {defaultValue}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error retrieving argument '{key}': {ex.Message}. Using default: {defaultValue}");
                }
            }

            return defaultValue;
        }

        public virtual TExpectedType DeserializeTypeFromJsonInput<TExpectedType, TResultValue>(string jsonInput, CommandInput input, ref LaunchPadMethodResult<TResultValue> methodResult)
            where TResultValue : class, ILaunchPadMethodResultValue
        {
            TExpectedType deserializedObject = default;
            try
            {
                deserializedObject = JsonSerializer.Deserialize<TExpectedType>(jsonInput);
            }
            catch (JsonException jEx)
            {
                methodResult.AddError("A JsonException occurred while deserializing input arguments:" + jEx.Message);
            }
            catch (NotSupportedException nsEx)
            {
                methodResult.AddError("A NotSupportedException error occurred while deserializing input arguments:" + nsEx.Message);
            }
            if (EqualityComparer<TExpectedType>.Default.Equals(deserializedObject, default))
            {
                methodResult.AddError($"Deserialized object is null or default from {jsonInput}.");
            }
            return deserializedObject;
        }

        // Helper method to retrieve and deserialize arguments
        public static async Task<TOutput> GetArgumentAsync<TResultValue, TInput, TOutput>(CommandInput input, string key, LaunchPadMethodResult<TResultValue> methodResult)
             where TResultValue : class, ILaunchPadMethodResultValue
        {
            // Retrieve the argument as the input type (e.g., string or MethodComplexity)
            var getArgumentResult = await input.Args.GetAsync<TInput>(key).ConfigureAwait(false);

            // Log success and error counts
            foreach (var error in getArgumentResult.Errors)
            {
                methodResult.AddError($"Error retrieving {key}: {error.Message}");
            }

            // Check if the retrieval was successful
            if (!getArgumentResult.IsSuccess)
            {
                methodResult.AddError($"Failed to retrieve argument '{key}'.");
            }

            // Retrieve the raw value
            TInput rawValue = getArgumentResult.ValueOrDefault;

            // Ensure the raw value is not null
            if (EqualityComparer<TInput>.Default.Equals(rawValue, default))
            {
                methodResult.AddError($"Argument '{key}' cannot be null or default.");
                return default;
            }

            try
            {
                // If TInput and TOutput are the same, return the value directly
                if (typeof(TInput) == typeof(TOutput))
                {
                    return (TOutput)(object)rawValue;
                }

                // If TOutput is an enum, parse the raw value into the enum type
                if (typeof(TOutput).IsEnum)
                {
                    return (TOutput)Enum.Parse(typeof(TOutput), rawValue.ToString(), ignoreCase: true);
                }

                // If TOutput is a primitive type or string, return the value directly
                if (typeof(TOutput).IsPrimitive || typeof(TOutput) == typeof(string))
                {
                    return (TOutput)Convert.ChangeType(rawValue, typeof(TOutput));
                }

                // Otherwise, deserialize the raw value into the expected output type
                string rawJson = rawValue.ToString();
                return JsonSerializer.Deserialize<TOutput>(rawJson, CommandBase.JsonOptions);
            }
            catch (JsonException ex)
            {
                methodResult.AddError($"Invalid JSON for argument '{key}': {rawValue}. Error: {ex.Message}");
            }
            catch (Exception ex) when (ex is InvalidCastException || ex is FormatException || ex is ArgumentException)
            {
                methodResult.AddError($"Failed to process argument '{key}': {rawValue}. Error: {ex.Message}");
            }
            return default;
        }

    }
}
