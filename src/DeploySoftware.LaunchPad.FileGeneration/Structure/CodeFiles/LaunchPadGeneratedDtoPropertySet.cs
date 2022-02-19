using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
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
        public virtual string Id { get; set; }

        /// <summary>
        /// A dictionary containing the names of the base properties that will be required
        /// </summary>
        public virtual IDictionary<string, string> RequiredBasePropertyNames { get; set; }

        /// <summary>
        /// A dictionary containing the names of the base properties that will be optional
        /// </summary>
        public virtual IDictionary<string, string> OptionalBasePropertyNames { get; set; }

        public virtual IDictionary<string, string> RequiredProperties => new DictionaryHelper().MergeDictionaries(RequiredBasePropertyNames, RequiredCustomPropertyNames);

        /// <summary>
        /// A dictionary containing the names of the custom properties that will be required
        /// </summary>
        public virtual IDictionary<string, string> RequiredCustomPropertyNames { get; set; }

        /// <summary>
        /// A dictionary containing the names of the custom properties that will be optional
        /// </summary>
        public virtual IDictionary<string, string> OptionalCustomPropertyNames { get; set; }

        public virtual IDictionary<string, string> OptionalProperties => new DictionaryHelper().MergeDictionaries(OptionalBasePropertyNames, OptionalCustomPropertyNames);

        public LaunchPadGeneratedDtoPropertySet()
        {
            Id = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            RequiredBasePropertyNames = new Dictionary<string, string>(comparer);
            OptionalBasePropertyNames = new Dictionary<string, string>(comparer);
            RequiredCustomPropertyNames = new Dictionary<string, string>(comparer);
            OptionalCustomPropertyNames = new Dictionary<string, string>(comparer);
        }
    }
}
