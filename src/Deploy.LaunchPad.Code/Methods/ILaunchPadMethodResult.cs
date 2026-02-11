using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Methods
{
    public partial interface ILaunchPadMethodResult
    {
        object? Value { get; } // implementers should make this protected and provide a protected setter. Access should be via the GetValue or GetValueOrDefault methods

        public object? UnderlyingResult { get; }

        public bool Succeeded { get; }

        public IReadOnlyDictionary<string, string> Errors { get; }

        public IReadOnlyDictionary<string, string> Warnings { get; }

        public IReadOnlyDictionary<string, string> Successes { get; }


        // Method to add an Error to the result
        public void AddError(string error);

        // Method to add multiple Error to the result
        public void AddErrors(IEnumerable<string> errors);
        public void AddErrors(IDictionary<string, string> errors);

        // Method to add a warning to the result
        public void AddWarning(string message);

        // Method to add multiple warnings to the result
        public void AddWarnings(IEnumerable<string> messages);

        // Method to add multiple warnings to the result
        public void AddWarnings(IDictionary<string,string> messages);

        // Method to add a success message to the result
        public void AddSuccess(string success);

        // Method to add multiple Successes to the result
        public void AddSuccesses(IEnumerable<string> successes);
        public void AddSuccesses(IDictionary<string, string> successes);
        
        // Methods to access the underlying FluentResult or FluentValidation object
        public TUnderlyingValueType GetValue<TUnderlyingValueType>(); // convenience method to get the Value strongly typed from the TUnderlyingFluentResultOrValidation object. Throws if type is wrong.

        public  TUnderlyingValueType? GetValueOrDefault<TUnderlyingValueType>(); // convenience method to get the Value strongly typed from the TUnderlyingFluentResultOrValidation object. Always returns a value or default.

        public TUnderlyingFluentResultOrValidation GetUnderlyingResult<TUnderlyingFluentResultOrValidation>(); // Exposes the original object (FluentResult or FluentValidation currently)

    }
}
