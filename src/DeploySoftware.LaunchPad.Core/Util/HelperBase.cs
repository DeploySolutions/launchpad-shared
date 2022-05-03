using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.AbpModuleConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public abstract class HelperBase : IHelper
        
    {
        public ILogger Logger { get; set; } = NullLogger.Instance;

        protected readonly IConfigurationRoot _configurationRoot;
        public IConfigurationRoot ConfigurationRoot { get { return _configurationRoot; } }



        protected HelperBase()
        {

        }

        public HelperBase(ILogger logger)
        {
            if(logger == null)
            {
                Logger = NullLogger.Instance;
            }
            Logger = logger;
        }

        public HelperBase(ILogger logger, IConfigurationRoot configurationRoot)
        {
            if (logger == null)
            {
                Logger = NullLogger.Instance;
            }
            Logger = logger;
            _configurationRoot = configurationRoot;
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