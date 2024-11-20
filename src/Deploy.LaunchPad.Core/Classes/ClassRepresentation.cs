using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Classes
{
    public partial class ClassRepresentation
    {
        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }
        public virtual Dictionary<string, PropertyRepresentation> Properties { get; set; }
        public virtual Dictionary<string, MethodRepresentation> Methods { get; set; }

        public ClassRepresentation(string name)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            Properties = new Dictionary<string, PropertyRepresentation>();
            Methods = new Dictionary<string, MethodRepresentation>();
        }
    }
}
