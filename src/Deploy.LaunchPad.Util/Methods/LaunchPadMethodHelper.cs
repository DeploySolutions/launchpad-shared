using Castle.Core.Logging;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Deploy.LaunchPad.Util.Methods
{
    public partial class LaunchPadMethodHelper : HelperBase
    {
        public LaunchPadMethodHelper() : base()
        {

        }
        
         /// <summary>
         /// Initializes a new instance of the <see cref="RandomGenerationHelper"/> class.
         /// </summary>
         /// <param name="logger">The logger.</param>
        public LaunchPadMethodHelper(ILogger logger) : base(logger)
        {

        }

        /// <summary>
        /// Merge two LaunchPadMethodResult together. The values in the secondary are added to the primary.
        /// </summary>
        /// <typeparam name="TResultValuePrimary"></typeparam>
        /// <typeparam name="TResultValueSecondary"></typeparam>
        /// <param name="primaryResult"></param>
        /// <param name="secondaryResult"></param>
        /// <returns></returns>
        public LaunchPadMethodResult<TResultValuePrimary> MergeMethodResults<TResultValuePrimary, TResultValueSecondary>(LaunchPadMethodResult<TResultValuePrimary> primaryResult, LaunchPadMethodResult<TResultValueSecondary> secondaryResult)
            where TResultValuePrimary : class, ILaunchPadMethodResultValue
            where TResultValueSecondary : class, ILaunchPadMethodResultValue
        {
            Guard.Against<ArgumentNullException>(primaryResult == null, "primaryResult cannot be null.");
            Guard.Against<ArgumentNullException>(secondaryResult == null, "secondaryResult cannot be null.");
            foreach (var success in secondaryResult.Successes.Values)
            {
                primaryResult.AddSuccess(success);
                Logger.Debug("Merging secondaryResult.Success: " + success);
            }
            foreach (var success in secondaryResult.UnderlyingResult.Successes)
            {
                primaryResult.UnderlyingResult?.WithSuccess(success);
                Logger.Debug("Merging secondaryResult.UnderlyingResult.Success: " + success);
            }
            foreach (var error in secondaryResult.Errors.Values)
            {
                primaryResult.AddError(error);
                Logger.Debug("Merging secondaryResult.Error: " + error);
            }
            foreach (var error in secondaryResult.UnderlyingResult.Errors)
            {
                primaryResult.UnderlyingResult?.WithError(error);
                Logger.Debug("Merging secondaryResult.UnderlyingResult.Error: " + error);
            }
            foreach (var warning in secondaryResult.Warnings.Values)
            {
                primaryResult.AddWarning(warning);
                Logger.Debug("Merging secondaryResult.Warning: " + warning);
            }
            return primaryResult;
        }
    }
}
