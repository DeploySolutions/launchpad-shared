using Castle.Core.Logging;
using Deploy.LaunchPad.FactoryLite.Methods;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.CommandLine;
using Deploy.LaunchPad.Util.Helpers;
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

        public virtual CommandHelper CommandHelper { get; init; }
        public virtual LaunchPadMethodHelper MethodHelper { get; init; }
        public virtual ErrorHandlingHelper ErrorHandlingHelper { get; init; }

        // Define event handlers
        public event EventHandler<MethodEventArgs>? MethodStart;
        public event EventHandler<MethodEventArgs>? MethodEnd; 
        public event EventHandler<ErrorEventArgs>? OnError;


        protected CommandBase(ILogger logger, TimeProvider clock)
        {
            Logger = logger;
            _clock = clock;
            CommandHelper = new CommandHelper(Logger);
            MethodHelper = new LaunchPadMethodHelper(Logger);
            ErrorHandlingHelper = new ErrorHandlingHelper(Logger);
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


        public virtual LaunchPadMethodResult<TResultValue> HandleError<TResultValue>(
             CommandInput input,
             Exception ex)
             where TResultValue : class, ILaunchPadMethodResultValue
        {
            return HandleError<TResultValue>(input, ex, ex.Message);
        }

        public virtual LaunchPadMethodResult<TResultValue> HandleError<TResultValue>(
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
