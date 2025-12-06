using Castle.Core.Logging;
using Deploy.LaunchPad.FactoryLite.Methods;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.CommandLine;
using Deploy.LaunchPad.Util.Methods;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FactoryLite.CommandLine
{
    public abstract partial class CommandBase : Deploy.LaunchPad.Util.CommandLine.ICommand
    {
        protected readonly TimeProvider _clock;

        protected ILogger Logger { get; init; } = NullLogger.Instance;

        public ElementNameLight Name { get; init; }
        public ElementDescriptionLight Description { get; init; }
        public virtual IReadOnlyList<OptionDefinition> Options { get; }

        // Define event handlers
        public event EventHandler<MethodEventArgs>? MethodStart;
        public event EventHandler<MethodEventArgs>? MethodEnd; 
        public event EventHandler<ErrorEventArgs>? OnError;


        protected CommandBase(ILogger logger, TimeProvider clock)
        {
            Logger = logger;
            _clock = clock;
            // Subscribe to various events
            MethodStart += (sender, args) =>
            {
                if (args.Parameters != null)
                {
                    Logger.Debug($"{args.ClassName}.{args.MethodName}() started with parameters: {args.Parameters}");
                }
                else
                {
                    Logger.Debug($"{args.ClassName}.{args.MethodName}() started.");
                }
            };

            MethodEnd += (sender, args) =>
            {
                Logger.Debug($"{args.ClassName}.{args.MethodName}() ended.");
            };
            
        }



        public abstract Task<LaunchPadMethodResult<TResultValue>> ExecuteAsync<TCommand, TResultValue>(CommandInput input)
            where TCommand : Deploy.LaunchPad.Util.CommandLine.ICommand
            where TResultValue : class, ILaunchPadMethodResultValue;

        /// <summary>
        /// Validates input, handles exceptions, and prepares results for commands.
        /// </summary>
        /// <typeparam name="TInputValue">The type of the input value.</typeparam>
        /// <typeparam name="TResultValue">The type of the result value.</typeparam>
        /// <param name="input">The command input.</param>
        /// <param name="inputValue">The input value to validate.</param>
        /// <param name="validator">The validator for the input value.</param>
        /// <param name="methodResult">The method result to update with validation information.</param>
        /// <returns>A boolean indicating whether the validation succeeded.</returns>
        protected async Task<bool> ValidateAndPrepareResultAsync<TInputValue, TResultValue>(
            CommandInput input,
            TInputValue inputValue,
            IValidator<TInputValue> validator,
            LaunchPadMethodResult<TResultValue> methodResult)
            where TInputValue : LaunchPadMethodResultValueBase
            where TResultValue : LaunchPadMethodResultValueBase
        {
            // Validate the input
            var inputValidationResult = validator.Validate(inputValue);
            methodResult.ConsolidateValidationResult(inputValidationResult);

            if (!inputValidationResult.IsValid)
            {
                string inputValidationErrorMessage = $"Input validation failed. Errors: {string.Join("; ", inputValidationResult.Errors.Select(e => e.ErrorMessage))}";

                if (input.CustomExceptionHandler != null)
                {
                    // Use the custom exception handler
                    var customResult = (LaunchPadMethodResult<TResultValue>)input.CustomExceptionHandler(new InvalidOperationException(inputValidationErrorMessage));
                    methodResult.AddErrors(customResult.Errors.Values);
                }
                else if (input.ExceptionHandling == ExceptionHandlingStrategy.ThrowException)
                {
                    throw new InvalidOperationException(inputValidationErrorMessage);
                }
                else
                {
                    // Default behavior: Add validation errors to the method result
                    methodResult.AddErrors(inputValidationResult.Errors.Select(e => e.ErrorMessage));
                }

                return false;
            }

            return true;
        }

        // Raise the MethodStart event
        protected virtual void OnMethodStart(string className, string methodName, object? parameters = null)
        {
            MethodStart?.Invoke(this, new MethodEventArgs(className, methodName, parameters));
        }

        // Raise the MethodEnd event
        protected virtual void OnMethodEnd(string className, string methodName)
        {
            MethodEnd?.Invoke(this, new MethodEventArgs(className, methodName, null));
        }

        protected virtual void OnMethodStartHandler(object? sender, MethodEventArgs args)
        {
            if (args.Parameters != null)
            {
                Logger.Debug($"Method {Name.Full} started with parameters: {args.Parameters}");
            }
            else
            {
                Logger.Debug($"Method {Name.Full} started.");
            }
        }

        protected virtual void OnMethodEndHandler(object? sender, MethodEventArgs args)
        {
            Logger.Debug($"Method {Name.Full} ended.");
        }

        protected virtual LaunchPadMethodResult<TResultValue> HandleError<TResultValue>(
             CommandInput input,
             Exception ex)
             where TResultValue : class, ILaunchPadMethodResultValue
        {
            return HandleError<TResultValue>(input, ex, ex.Message);
        }

        protected virtual LaunchPadMethodResult<TResultValue> HandleError<TResultValue>(
             CommandInput input,
             Exception ex,
             string errorMessage)
             where TResultValue : class, ILaunchPadMethodResultValue
        {
            // Log the start of the error handling
            OnMethodStart(this.GetType().Name, nameof(HandleError), new { Exception = ex, ErrorMessage = errorMessage });

            var args = new ErrorEventArgs(errorMessage, ex);
            OnError?.Invoke(this, args);

            LaunchPadMethodResult<TResultValue> result;

            if (args.Handled)
            {
                result = (LaunchPadMethodResult<TResultValue>)args.Result!;
            }
            else
            {
                Logger.Error(errorMessage, ex);

                if (input.CustomExceptionHandler != null)
                {
                    result = (LaunchPadMethodResult<TResultValue>)input.CustomExceptionHandler(new InvalidOperationException(errorMessage, ex));
                }
                else if (input.ExceptionHandling == ExceptionHandlingStrategy.ThrowException)
                {
                    throw new InvalidOperationException(errorMessage, ex);
                }
                else
                {
                    // Create the result object using a constructor or factory method
                    var resultValue = Activator.CreateInstance<TResultValue>();
                    var failResult = Result.Fail<TResultValue>(new Error(errorMessage));
                    result = new LaunchPadMethodResult<TResultValue>(failResult);
                }
            }

            // Log the end of the error handling
            OnMethodEnd(this.GetType().Name, nameof(HandleError));

            return result;
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
        protected virtual bool GetBoolValueFromArgs(CommandInput input, string key, bool defaultValue)
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
        protected virtual string GetStringValueFromArgs(CommandInput input, string key, string defaultValue)
        {
            Guard.Against<ArgumentNullException>(input == null, "CommandBase.GetBoolValueFromArgs() => input cannot be null.");
            Guard.Against<ArgumentNullException>(input.Args == null, "CommandBase.GetBoolValueFromArgs() => input.Args cannot be null.");
            if (input.Args.ContainsKey(key))
            {
                return input.Args.Get<string>(key).ValueOrDefault;
            }

            // Fallback to the default value from Options
            var option = Options.FirstOrDefault(o => o.Name.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (option != null && option.DefaultValue is string defaultFromOptions)
            {
                return defaultFromOptions;
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
        protected int GetIntValueFromArgs(CommandInput input, string key, int defaultValue)
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
        protected virtual long GetLongValueFromArgs(CommandInput input, string key, long defaultValue)
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
        protected virtual DateTime GetDateTimeValueFromArgs(CommandInput input, string key, DateTime defaultValue, DateTimeKind kind = DateTimeKind.Utc)
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
    }

    public class ErrorEventArgs : EventArgs
    {
        public string ErrorMessage { get; }
        public Exception Exception { get; }
        public object? Result { get; set; }
        public bool Handled { get; set; }

        public ErrorEventArgs(string errorMessage, Exception exception)
        {
            ErrorMessage = errorMessage;
            Exception = exception;
            Handled = false;
        }
    }
}
