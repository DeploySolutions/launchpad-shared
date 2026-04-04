using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Methods
{
    public partial class TestPocoValidator : LaunchPadValidatorBase<TestPoco>
    {
        public TestPocoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .WithSeverity(Severity.Error);
            RuleFor(x => x.Name.Name)
                .NotEmpty().WithMessage("Full Name is required.")
                .WithSeverity(Severity.Error);
            RuleFor(x => x.Name.Name)
                .MaximumLength(5).WithMessage("Full Name should not exceed 5 characters.")
                .WithSeverity(Severity.Warning);
            RuleFor(x => x.Name.ShortName)
                .MaximumLength(5).WithMessage("Short Name cannot exceed 5 characters.")
                .WithSeverity(Severity.Error);
        }
    }
}
