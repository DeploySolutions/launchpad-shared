using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public partial class LaunchPadGeneratedModule<TModuleSettings> : LaunchPadGeneratedObjectBase
        where TModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
    {
        public TModuleSettings Settings { get; set; }

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

        public LaunchPadGeneratedModule() : base()
        {
            Settings = new TModuleSettings();
        }
    }
}
