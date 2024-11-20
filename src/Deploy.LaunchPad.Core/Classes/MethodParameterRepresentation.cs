using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Classes
{
    public partial class MethodParameterRepresentation
    {
        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }
        public virtual ElementType ParameterType { get; set; }

        public MethodParameterRepresentation(string name, ElementType parameterType)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            ParameterType = parameterType;
        }
    }
}
