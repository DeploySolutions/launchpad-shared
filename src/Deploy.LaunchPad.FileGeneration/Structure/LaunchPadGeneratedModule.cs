using Castle.Core.Logging;
using Deploy.LaunchPad.FileGeneration.Stages;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public partial class LaunchPadGeneratedModule<TModuleSettings> : LaunchPadGeneratedObjectBase,
        ILaunchPadGeneratedModule<TModuleSettings>
        where TModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
    {
        public virtual ILogger Logger { get; set; }

        public virtual TModuleSettings Settings { get; set; }

        public virtual IDictionary<string, ILicensedThirdPartySoftwareItem> LicensedThirdPartyItems { get; set; }


        /// <summary>
        /// Returns a bool indicating if the module is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the module is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public virtual bool CheckValidity()
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(ObjectTypeName)
                  && Settings != null
            )
            {
                isValid = true;
            }
            return isValid;
        }

        public LaunchPadGeneratedModule(ILogger logger) : base()
        {
            Logger = logger;
            Settings = new TModuleSettings(); 
            var comparer = StringComparer.OrdinalIgnoreCase;
            LicensedThirdPartyItems = new Dictionary<string, ILicensedThirdPartySoftwareItem>(comparer);
        }
    }
}
