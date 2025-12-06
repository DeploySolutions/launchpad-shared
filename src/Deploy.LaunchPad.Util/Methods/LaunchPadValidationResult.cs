using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Util.Methods
{
    public partial class LaunchPadValidationResult<TValueType> : LaunchPadMethodResultBase
    {
        private readonly ValidationResult _validationResult;

        public override bool Succeeded => !_validationResult.Errors.Any(e => e.Severity == Severity.Error);

        public override ValidationResult UnderlyingResult => _validationResult; // Strongly typed access

        protected TValueType _value;

        public override object? Value => _value;

        public LaunchPadValidationResult(ValidationResult validationResult, TValueType value) : base(validationResult)
        {
           
            // uses a FluentValidation result
            _validationResult = validationResult;
            _value = value;
            foreach (var error in validationResult.Errors.Where(e => e.Severity == Severity.Error))
            {
                _errors.TryAdd(error.ErrorCode, error.ErrorMessage);
            }
            foreach (var error in validationResult.Errors.Where(e => e.Severity == Severity.Warning))
            {
                _warnings.TryAdd(error.ErrorCode, error.ErrorMessage);
            }
        }


        // Method to add a warning to the result
        public override void AddWarning(string warningMessage)
        {
            _warnings.TryAdd(warningMessage, warningMessage);
        }

        // Method to add multiple warnings to the result
        public override void AddWarnings(IEnumerable<string> warningMessages)
        {
            foreach (var warningMessage in warningMessages)
            {
                _warnings.TryAdd(warningMessage, warningMessage);
            }
        }

        // Method to add multiple warnings to the result
        public override void AddWarnings(IDictionary<string, string> warningMessages)
        {
            foreach (var warningMessage in warningMessages)
            {
                _warnings.TryAdd(warningMessage.Key, warningMessage.Value);
            }
        }

        // Method to add a Error to the result
        public override void AddError(string message)
        {
            _errors.TryAdd(message, message);
        }

        // Method to add multiple Errors to the result
        public override void AddErrors(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                _errors.TryAdd(message, message);
            }
        }

        // Method to add multiple Errors to the result
        public override void AddErrors(IDictionary<string, string> messages)
        {
            foreach (var message in messages)
            {
                _errors.TryAdd(message.Key, message.Value);
            }
        }


        // Method to add a Success message to the result
        public override void AddSuccess(string message)
        {
            _successes.TryAdd(message, message);
        }

        // Method to add multiple Successes to the result
        public override void AddSuccesses(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                _successes.TryAdd(message, message);
            }
        }

        // Method to add multiple Successes to the result
        public override void AddSuccesses(IDictionary<string,string> messages)
        {
            foreach (var message in messages)
            {
                _successes.TryAdd(message.Key, message.Value);
            }
        }
    }

}
