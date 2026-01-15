using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Util.Methods
{
    public partial class LaunchPadMethodResult<TResultValue> : LaunchPadMethodResultBase
        where TResultValue: class, ILaunchPadMethodResultValue
    {
        private readonly Result<TResultValue> _fluentResult;

        private static readonly StringComparer Comparer = StringComparer.OrdinalIgnoreCase;

        private ValidationResult? _validationResult;
        public ValidationResult? ValidationResult
        {
            get => _validationResult;
            private set => _validationResult = value;
        }
       
        public override bool Succeeded
        {
            get
            {
                bool validationSucceeded = ValidationResult == null || !ValidationResult.Errors.Any(e => e.Severity == Severity.Error);
                bool operationSucceeded = _fluentResult?.IsSuccess ?? false;
                return validationSucceeded && operationSucceeded;
            }
        }

        public override object? Value
        {
            get
            {
                if (_fluentResult.IsSuccess)
                {
                    return _fluentResult.Value;
                }
                else
                {
                    // Optionally log or handle the failed state here
                    return null;
                }
            }
        }

        public override Result<TResultValue> UnderlyingResult => _fluentResult;

        /// <summary>
        /// Creates a new LaunchPadMethodResult for use in a method return, using a FluentResults object.
        /// 
        /// </summary>
        /// <param name="result"></param>
        public LaunchPadMethodResult(Result<TResultValue> result) : base(result)
        {
            // uses a FluentResults result
            _fluentResult = result;

            // Only set Value if the underlying result is successful
            if (_fluentResult.IsSuccess)
            {
                try
                {
                    Value = _fluentResult.Value;
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    AddError($"Failed to retrieve value: {ex.Message}");
                    Value = null; // Leave Value as null if an exception occurs
                }
            }
            else
            {
                Value = null; // Leave Value as null if the result is not successful
            }
            AggregateResultData(_fluentResult, null);
        }

        /// <summary>
        /// Creates a new LaunchPadMethodResult for use in a method return, using a FluentResults object.
        /// 
        /// </summary>
        /// <param name="result"></param>
        public LaunchPadMethodResult(Result<TResultValue> result, LaunchPadValidationResult<TResultValue> validationResult) : base(result)
        {
            // uses a FluentResults result
            _fluentResult = result;
            _validationResult = validationResult.UnderlyingResult;
            Value = _fluentResult.Value;
            AggregateResultData(_fluentResult, validationResult);
        }

        // Defensive: Only add if key does not exist
        protected void AggregateResultData(Result<TResultValue> fluentResult, LaunchPadValidationResult<TResultValue>? validationResult)
        {
            if (validationResult != null)
            {
                foreach (var error in validationResult.Errors)
                    AddErrorToCollection(error.Key, error.Value);
                foreach (var warning in validationResult.Warnings)
                    AddWarningToCollection(warning.Key, warning.Value);
                foreach (var success in validationResult.Successes)
                    AddSuccessToCollection(success.Key, success.Value);
            }
            foreach (var error in fluentResult.Errors)
            {
                AddErrorToCollection(error.Message, error.Message);
            }
            foreach (var warning in fluentResult.Reasons.Where(r => r is Warning))
            {
                AddWarningToCollection(warning.Message, warning.Message);
            }
            foreach (var success in fluentResult.Reasons.OfType<Success>().Where(s => !(s is Warning)))
            {
                AddSuccessToCollection(success.Message, success.Message);
            }
        }

        public override void AddWarning(string message)
        {
            _fluentResult.WithReason(new Warning(message));
            AddWarningToCollection(message, message);
        }

        public override void AddWarnings(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                _fluentResult.WithReason(new Warning(message));
                AddWarningToCollection(message, message);
            }
        }

        public override void AddWarnings(IDictionary<string, string> messages)
        {
            foreach (var message in messages)
            {
                _fluentResult.WithReason(new Warning(message.Value));
                AddWarningToCollection(message.Key, message.Value);
            }
        }

        public override void AddError(string error)
        {
            _fluentResult.WithReason(new Error(error));
            AddErrorToCollection(error, error);
        }

        public override void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                _fluentResult.WithReason(new Error(error));
                AddErrorToCollection(error, error);
            }
        }

        public override void AddErrors(IDictionary<string, string> errors)
        {
            foreach (var error in errors)
            {
                _fluentResult.WithReason(new Error(error.Value));
                AddErrorToCollection(error.Key, error.Value);
            }
        }

        public override void AddSuccess(string success)
        {
            _fluentResult.WithReason(new Success(success));
            AddSuccessToCollection(success, success);
        }

        public override void AddSuccesses(IEnumerable<string> successes)
        {
            foreach (var success in successes)
            {
                _fluentResult.WithReason(new Success(success));
                AddSuccessToCollection(success, success);
            }
        }
        public override void AddSuccesses(IDictionary<string, string> successes)
        {
            foreach (var success in successes)
            {
                _fluentResult.WithReason(new Success(success.Value));
                AddSuccessToCollection(success.Key, success.Value);
            }
        }

        public void ConsolidateValidationResult(ValidationResult validationResult)
        {
            Guard.Against<ArgumentNullException>(validationResult == null, "validationResult cannot be null.");
            if (_validationResult == null)
            {
                _validationResult = validationResult;
            }
            else
            {
                // Combine errors and warnings from both validation results
                foreach (var error in validationResult.Errors)
                {
                    if (!_validationResult.Errors.Any(e => e.ErrorMessage == error.ErrorMessage))
                    {
                        _validationResult.Errors.Add(error);
                    }
                }
            }

            // Add errors to the Errors collection
            foreach (var error in validationResult.Errors)
            {
                AddError(error.ErrorMessage);
            }
        }

        /// <summary>
        // Generic method to combine validation and operation results into a single result.
        // Since the validation may succeed and/or return warnings, and we may then proceed to an operation that fails,
        // this method combines the results of both into a single result to ensure nothing is lost.
        /// </summary>
        /// <typeparam name="TValidation"></typeparam>
        /// <typeparam name="TOperation"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="launchPadValidationResult"></param>
        /// <param name="fluentResultFromMethod"></param>
        /// <param name="successFactory"></param>
        /// <returns></returns>
        public static Result<U> CombineResults<TValidation, TOperationResult, U>(
            LaunchPadValidationResult<TValidation> launchPadValidationResult,
            Result<TOperationResult> fluentResultFromMethod,
            Func<TOperationResult, U> successFactory)
        {
            var combinedResult = new Result<U>();

            // Add validation errors and warnings
            if (!launchPadValidationResult.Succeeded)
            {
                combinedResult = Result.Fail<U>("Validation failed")
                    .WithErrors(launchPadValidationResult.Errors.Select(e => new Error(e.Value)))
                    .WithReasons(launchPadValidationResult.Warnings.Select(w => new Warning(w.Value)));
            }

            // Add operation errors and warnings
            if (fluentResultFromMethod.IsFailed)
            {
                combinedResult = Result.Fail<U>(fluentResultFromMethod.Errors.First().Message)
                    .WithReasons(fluentResultFromMethod.Reasons);
            }

            // If both validation and operation succeeded, return success
            if (launchPadValidationResult.Succeeded && fluentResultFromMethod.IsSuccess)
            {
                combinedResult = Result.Ok<U>(successFactory(fluentResultFromMethod.Value))
                    .WithReasons(launchPadValidationResult.Warnings.Select(w => new Warning(w.Value)))
                    .WithReasons(fluentResultFromMethod.Reasons);
            }

            return combinedResult;
        }

        public static explicit operator LaunchPadMethodResult<TResultValue>(LaunchPadMethodResult<LaunchPadMethodResultValueBase> v)
        {
            throw new NotImplementedException();
        }
    }
}
