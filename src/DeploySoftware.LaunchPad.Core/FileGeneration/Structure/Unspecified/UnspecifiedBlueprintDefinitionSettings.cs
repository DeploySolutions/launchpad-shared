using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class UnspecifiedBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {

        /// <summary>
        /// The list of setting Key Value Pairs
        /// </summary>
        public virtual IDictionary<string, string> Settings { get; set; }


        public UnspecifiedBlueprintDefinitionSettings() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Settings = new Dictionary<string, string>(comparer);
        }
    }
}
