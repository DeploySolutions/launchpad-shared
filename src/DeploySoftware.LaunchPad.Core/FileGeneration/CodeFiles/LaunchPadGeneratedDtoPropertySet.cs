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
        /// A list of property names
        /// </summary>
        public IList<string> PropertyNames { get; set; }

        public LaunchPadGeneratedDtoPropertySet()
        {
            Id = string.Empty;
            PropertyNames = new List<string>();
        }
    }
}
