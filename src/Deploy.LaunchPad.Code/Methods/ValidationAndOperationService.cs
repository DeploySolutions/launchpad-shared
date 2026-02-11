using Deploy.LaunchPad.Util;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace Deploy.LaunchPad.Code.Methods
{


    public class ValidationAndOperationService
    {

        public ValidationAndOperationService()
        {
        }

        public LaunchPadMethodResult<ValidateAndPerformOperationOutput> ValidateAndPerformOperation(ValidateAndPerformOperationInput input)
        {
            LaunchPadMethodResult<ValidateAndPerformOperationOutput> output;
            Result<ValidateAndPerformOperationOutput> result;

            // Step 1: Validate the input using complex rules
            var inputValidator = new ValidateAndPerformOperationInputValidator();
            var inputValidationResult = inputValidator.ValidateMethodInput(input);

            // Step 2: Validate the domain entity
            var domainValidator = new TestPocoValidator();
            var domainValidationResult = domainValidator.ValidateWithWarnings(input.TestPoco);

            // Combine both validation results
            var combinedErrors = inputValidationResult.Errors.Concat(domainValidationResult.Errors).ToList();
            var combinedWarnings = inputValidationResult.Warnings.Concat(domainValidationResult.Warnings).ToList();
            var combinedValidationResult = new ValidationResultWithWarnings(
                new ValidationResult(combinedErrors.Concat(combinedWarnings).ToList()),
                combinedErrors,
                combinedWarnings
            );

            var validationAdapter = new LaunchPadValidationResult<TestPoco>(combinedValidationResult.ValidationResult, input.TestPoco);
            if (!validationAdapter.Succeeded)
            {
                var defaultOutput = new ValidateAndPerformOperationOutput(input.TestPoco);
                result = inputValidator.CreateResult(combinedValidationResult, defaultOutput);

                output = new LaunchPadMethodResult<ValidateAndPerformOperationOutput>(result);
                return output;
            }
            // Step 2: Perform some operation (mocked here)
            input.TestPoco.SeqNum += 10; // do some thing
            var operationResult = PerformOperation(input.TestPoco);

            // Combine validation results with operation results
            var combinedResult = LaunchPadMethodResult<ValidateAndPerformOperationOutput>.CombineResults(
                validationAdapter,
                operationResult,
                opResult => new ValidateAndPerformOperationOutput(opResult)
            );


            if (combinedResult.IsSuccess)
            {
                var operationOutput = new ValidateAndPerformOperationOutput( input.TestPoco);
                combinedResult = Result.Ok(operationOutput).WithReasons(combinedResult.Reasons);
            }
            output = new LaunchPadMethodResult<ValidateAndPerformOperationOutput>(combinedResult);
            return output;
        }

        private Result<TestPoco> PerformOperation(TestPoco entity)
        {
            // Mock operation logic
            if (entity.Name.Full == "something")
            {
                return Result.Fail<TestPoco>("Operation failed: Entity name cannot be something.");
            }

            // Successful operation
            return Result.Ok(entity);//.WithWarning("This is a sample warning from the operation.");
        }

    }

    public class ValidateAndPerformOperationInput
    {
        public TestPoco TestPoco { get; set; }
        public string SomeInput{ get; set; } = string.Empty;
        public ValidateAndPerformOperationInput(TestPoco item)
        {
            TestPoco = item;
        }
    }

    public class ValidateAndPerformOperationInputValidator : LaunchPadValidatorBase<ValidateAndPerformOperationInput>
    {
        public ValidateAndPerformOperationInputValidator()
        {
            RuleFor(x => x.TestPoco.Name).NotNull().WithMessage("TestPoco.Name is required.");
            RuleFor(x => x.TestPoco.Name.Full).NotEmpty().WithMessage("TestPoco.Name.Full cannot be empty.");
            RuleFor(x => x.SomeInput).NotNull().WithMessage("SomeInput is required.");
            RuleFor(x => x.TestPoco.SeqNum).InclusiveBetween(1, 3).WithMessage("For this complex validation, sequence number must be between 1 and 3.");
        }
    }

    public class ValidateAndPerformOperationOutput : LaunchPadMethodResultValueBase, ILaunchPadMethodResultValue
    {
        public TestPoco TestPoco { get; set; }
        public string SomeOutput { get; set; } = string.Empty;
        public ValidateAndPerformOperationOutput(TestPoco item)
        {
            TestPoco = item;
        }
    }
}