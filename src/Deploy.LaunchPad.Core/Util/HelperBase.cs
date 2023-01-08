using Castle.Core.Logging;
using System;
using System.ComponentModel;
using System.Linq;

namespace Deploy.LaunchPad.Core.Util
{
    public abstract class HelperBase : IHelper

    {
        public ILogger Logger { get; set; } = NullLogger.Instance;


        protected HelperBase()
        {

        }

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