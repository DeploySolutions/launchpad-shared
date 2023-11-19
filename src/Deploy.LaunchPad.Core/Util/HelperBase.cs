// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="HelperBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using System;
using System.ComponentModel;
using System.Linq;

namespace Deploy.LaunchPad.Core.Util
{
    /// <summary>
    /// Class HelperBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Util.IHelper" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Util.IHelper" />
    public abstract class HelperBase : IHelper

    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; } = NullLogger.Instance;


        /// <summary>
        /// Initializes a new instance of the <see cref="HelperBase"/> class.
        /// </summary>
        protected HelperBase()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelperBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HelperBase(ILogger logger)
        {
            if (logger != null)
            {
                Logger = logger;
            }
        }


        /// <summary>
        /// Returns the description of an enum value, if available. If not available, either return the original enum value, or empty string.
        /// Useful in cases where the enum value should have a period, hyphen, or other disallowed characters.
        /// </summary>
        /// <param name="value">The value of the enum</param>
        /// <param name="shouldReturnOriginalValueIfDescriptionEmpty">if set to <c>true</c> [should return original value if description empty].</param>
        /// <returns>A string containing the description attribute of that enum</returns>
        public string GetDescriptionFromEnum(Enum value, bool shouldReturnOriginalValueIfDescriptionEmpty = true)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            string description = string.Empty;

            // try to find the Description attribute
            DescriptionAttribute[] descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (descriptionAttributes != null && descriptionAttributes.Any())
            {
                // description found, return it
                description = descriptionAttributes.First().Description;
            }
            else
            {
                // nothing found, should we return the original value?
                if (shouldReturnOriginalValueIfDescriptionEmpty)
                {
                    description = value.ToString();
                }
            }
            return description;
        }


    }
}