using Deploy.LaunchPad.Core.Elements;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.CodeRepresentation
{
    public partial class MethodParameterRepresentation
    {
        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }
        public virtual ElementType ParameterType { get; set; }

        public virtual object? Value { get; set; }

        public MethodParameterRepresentation(string name, ElementType parameterType)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            ParameterType = parameterType;
        }
    }
}
