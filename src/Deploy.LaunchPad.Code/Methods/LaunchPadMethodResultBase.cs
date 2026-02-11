using FluentResults;
using FluentValidation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Code.Methods
{
    public abstract partial class LaunchPadMethodResultBase : ILaunchPadMethodResult
    {
        protected readonly object? _result;

        public virtual object? Value { get; protected set; }
        public virtual object? UnderlyingResult => _result; // Strongly typed access
    
        public virtual bool Succeeded { get; protected set; }

        protected readonly ConcurrentDictionary<string, string> _errors = new(StringComparer.OrdinalIgnoreCase);
        public IReadOnlyDictionary<string, string> Errors => _errors;

        protected readonly ConcurrentDictionary<string, string> _warnings = new(StringComparer.OrdinalIgnoreCase);
        public IReadOnlyDictionary<string, string> Warnings => _warnings;

        protected readonly ConcurrentDictionary<string, string> _successes = new(StringComparer.OrdinalIgnoreCase);
        public IReadOnlyDictionary<string, string> Successes => _successes;


        /// <summary>
        /// Creates a new LaunchPadMethodResult for use in a method return, using a FluentResults object.
        /// 
        /// </summary>
        /// <param name="result"></param>
        protected LaunchPadMethodResultBase(object result)
        {
            _result = result;
        }

        public virtual TUnderlyingValueType GetValue<TUnderlyingValueType>()
        {
            if (Value is TUnderlyingValueType tValue) return tValue;
            throw new InvalidCastException($"Value is not of type {typeof(TUnderlyingValueType).Name}");
        }

        public virtual TUnderlyingValueType? GetValueOrDefault<TUnderlyingValueType>()
        {
            if (Value is TUnderlyingValueType tValue)
                return tValue;
            return default;
        }

        public virtual TUnderlyingFluentResultOrValidation GetUnderlyingResult<TUnderlyingFluentResultOrValidation>()
        {
            if (_result is TUnderlyingFluentResultOrValidation tResult) return tResult;
            throw new InvalidCastException($"UnderlyingResult is not of type {typeof(TUnderlyingFluentResultOrValidation).Name}");
        }

        // Method to add an Error to the result
        public abstract void AddError(string error);

        // Method to add multiple Error to the result
        public abstract void AddErrors(IEnumerable<string> errors);
        public abstract void AddErrors(IDictionary<string, string> errors);

        // Method to add a warning to the result
        public abstract void AddWarning(string warning);

        // Method to add multiple warnings to the result
        public abstract void AddWarnings(IEnumerable<string> warnings);
        public abstract void AddWarnings(IDictionary<string, string> warnings);


        // Method to add a success message to the result
        public abstract void AddSuccess(string success);

        // Method to add multiple Successes to the result
        public abstract void AddSuccesses(IEnumerable<string> successes);
        public abstract void AddSuccesses(IDictionary<string, string> successes);

        /// <summary>
        /// Helper to add an error to the Errors collection if not already present.
        /// </summary>
        protected void AddErrorToCollection(string key, string value)
        {
            if (!_errors.ContainsKey(key)) _errors.TryAdd(key, value);
        }

        /// <summary>
        /// Helper to add a warning to the Warnings collection if not already present.
        /// </summary>
        protected void AddWarningToCollection(string key, string value)
        {
            if (!_warnings.ContainsKey(key)) _warnings.TryAdd(key, value);
        }

        /// <summary>
        /// Helper to add a success to the Successes collection if not already present.
        /// </summary>
        protected void AddSuccessToCollection(string key, string value)
        {
            if (!_successes.ContainsKey(key)) _successes.TryAdd(key, value);
        }


    }

    public class Warning : Success
    {
        public Warning(string message) : base(message)
        {
        }
    }
}
