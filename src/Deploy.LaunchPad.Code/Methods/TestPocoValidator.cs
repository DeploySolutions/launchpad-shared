using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Methods
{
    public class TestPocoValidator : LaunchPadValidatorBase<TestPoco>
    {
        public TestPocoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .WithSeverity(Severity.Error);
            RuleFor(x => x.Name.Full)
                .NotEmpty().WithMessage("Full Name is required.")
                .WithSeverity(Severity.Error);
            RuleFor(x => x.Name.Full)
                .MaximumLength(5).WithMessage("Full Name should not exceed 5 characters.")
                .WithSeverity(Severity.Warning);
            RuleFor(x => x.Name.Short)
                .MaximumLength(5).WithMessage("Short Name cannot exceed 5 characters.")
                .WithSeverity(Severity.Error);
        }
    }
}
