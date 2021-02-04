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
        public string Id { get; set; }

        /// <summary>
        /// A list of base property names to generate, or "all" to include all base properties
        /// </summary>
        public IList<string> BasePropertyNames { get; set; }

        /// <summary>
        /// A list of custom property names to generate, or "all" to include all custom properties
        /// </summary>
        public IList<string> CustomPropertyNames { get; set; }

        public LaunchPadGeneratedDtoPropertySet()
        {
            Id = string.Empty;
            BasePropertyNames = new List<string>();
            CustomPropertyNames = new List<string>();
        }
    }
}
