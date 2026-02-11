using Deploy.LaunchPad.Util;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Methods
{
    public abstract partial class LaunchPadValidatorBase<T> : AbstractValidator<T>
    {
        /// <summary>
        /// Validate and retrieve errors and warnings separately.
        /// </summary>
        public ValidationResultWithWarnings ValidateWithWarnings(T instance)
        {
            var validationResult = Validate(instance);

            var errors = validationResult.Errors
                .Where(e => e.Severity == Severity.Error)
                .ToList();

            var warnings = validationResult.Errors
                .Where(e => e.Severity == Severity.Warning)
                .ToList();

            return new ValidationResultWithWarnings(validationResult, errors, warnings);
        }

        public ValidationResultWithWarnings ValidateMethodInput(T input)
        {
            // Guard checks
            Guard.Against<ArgumentNullException>(input == null, "input cannot be null.");

            // input validation (if any)
            return ValidateWithWarnings(input);
        }

        /// <summary>
        /// Create a Result object from the validation result with warnings and errors.
        /// </summary>
        public Result<TOutput> CreateResult<TOutput>(ValidationResultWithWarnings validationResult, TOutput value)
        {
            if (validationResult.IsValid)
            {
                return Result.Ok(value);
            }

            var result = Result.Fail<TOutput>(validationResult.Errors.FirstOrDefault()?.ErrorMessage);

            foreach (var error in validationResult.Errors.Skip(1))
            {
                if (!result.Errors.Any(e => e.Message == error.ErrorMessage))
                {
                    result.WithError(error.ErrorMessage);
                }
            }

            foreach (var warning in validationResult.Warnings)
            {
                result.WithReason(new Warning(warning.ErrorMessage));
            }

            return result;
        }

    }


    /// <summary>
    /// Encapsulates validation results with errors and warnings separated.
    /// </summary>
    public class ValidationResultWithWarnings
    {
        public ValidationResult ValidationResult { get; }
        public IReadOnlyCollection<ValidationFailure> Errors { get; }
        public IReadOnlyCollection<ValidationFailure> Warnings { get; }

        public bool IsValid => !Errors.Any(); // Warnings do not affect validity

        public ValidationResultWithWarnings(
            ValidationResult validationResult,
            IEnumerable<ValidationFailure> errors,
            IEnumerable<ValidationFailure> warnings)
        {
            ValidationResult = validationResult;
            Errors = errors.ToList();
            Warnings = warnings.ToList();
        }
    }
}
