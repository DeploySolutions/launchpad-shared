using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a set of properties that can be generated for a Dto
    /// </summary>
    [Serializable]
    public partial class LaunchPadGeneratedDtoPropertySet
    {

        /// <summary>
        /// The unique identifier of this property set, which can be referred to by generated DTOs
        /// </summary>        
        public string Id { get; set; }

        /// <summary>
        /// If this value is set, it overrides all base property values contained within this set.
        /// </summary>
        public bool? AllBasePropertiesRequired { get; set; }

        /// <summary>
        /// If this value is set, it overrides all custom property values contained within this set.
        /// </summary>
        public bool? AllCustomPropertiesRequired { get; set; }

        /// <summary>
        /// A dictionary of base properties to generate
        /// </summary>
        public IDictionary<string, LaunchPadGeneratedProperty> BaseProperties { get; set; }

        /// <summary>
        /// A dictionary of custom properties to generate
        /// </summary>
        public IDictionary<string, LaunchPadGeneratedProperty> CustomProperties { get; set; }

        public LaunchPadGeneratedDtoPropertySet()
        {
            Id = string.Empty;
            BaseProperties = new Dictionary<string, LaunchPadGeneratedProperty>();
            CustomProperties = new Dictionary<string, LaunchPadGeneratedProperty>();
        }
    }
}
