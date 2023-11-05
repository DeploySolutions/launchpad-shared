using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure.CodeFiles
{
    public partial class Inheritance
    {
        public virtual string ParentFullyQualifiedType { get; set; } = string.Empty;

        /// <summary>
        /// If tracked/known, specify the fully qualified type names of any children entities (ex. MyCorp.MyApp.Orders.LineItems)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IDictionary<string,string> ChildrenFullyQualifiedTypes { get; }

        /// <summary>
        /// The class and interface inheritance of the item (everything after the colon ':' in the definition)
        /// </summary>
        public virtual IDictionary<string,string> InheritsFrom { get; set; }

        public Inheritance() {
            var comparer = StringComparer.OrdinalIgnoreCase;
            InheritsFrom = new Dictionary<string, string>(comparer);
            ChildrenFullyQualifiedTypes = new Dictionary<string, string>(comparer);
        }
    }
}
