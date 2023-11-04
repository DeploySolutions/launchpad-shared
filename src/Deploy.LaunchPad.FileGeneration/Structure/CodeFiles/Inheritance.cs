using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure.CodeFiles
{
    public partial class Inheritance
    {
        public virtual string ParentClass { get; set; }

        /// <summary>
        /// The class and interface inheritance of the item (everything after the colon ':' in the definition)
        /// </summary>
        public virtual IDictionary<string,string> InheritsFrom { get; set; }

        public Inheritance() {
            var comparer = StringComparer.OrdinalIgnoreCase;
            InheritsFrom = new Dictionary<string, string>(comparer);
        }
    }
}
