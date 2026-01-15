using Castle.Core.Logging;
using Deploy.LaunchPad.Util.Methods;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Util.Helpers
{
    public partial class ErrorHandlingHelper : HelperBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingHelper"/> class.
        /// </summary>
        public ErrorHandlingHelper() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingHelper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ErrorHandlingHelper(ILogger logger) : base(logger)
        {

        }

        /// <summary>
        /// Adds multiple errors to the result.Errors collection.
        /// </summary>
        /// <typeparam name="TResultValue">The type of the result value.</typeparam>
        /// <param name="methodResult">The method result to which errors will be added.</param>
        /// <param name="errors">The collection of errors to add.</param>
        public virtual void AddErrors<TResultValue>(LaunchPadMethodResult<TResultValue> methodResult, IDictionary<string, string> errors)
            where TResultValue : class, ILaunchPadMethodResultValue
        {
            methodResult.AddErrors(errors);
        }

        /// <summary>
        /// Log each error in the provided dictionary.
        /// </summary>
        /// <typeparam name="TResultValue"></typeparam>
        /// <param name="logger"></param>
        /// <param name="errors"></param>
        public virtual void LogErrors(ILogger logger, string caller, IReadOnlyList<IError> errors)
        {
            foreach (var error in errors)
            {
                logger.Error($"Error in {caller}: {error.Message}");
            }
        }
    }
}
