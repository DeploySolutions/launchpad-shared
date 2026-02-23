using Castle.Core.Logging;
using Deploy.LaunchPad.Util.Elements;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.CodeRepresentation
{
    public partial class MethodRepresentation
    {
        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }
        public virtual ElementType ReturnType { get; set; }
        public virtual List<MethodParameterRepresentation> Parameters { get; set; }

        public MethodRepresentation(string name, Type returnType)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            ReturnType = ElementType.GetTypeInformationForElement(NullLogger.Instance, returnType);
            Parameters = new List<MethodParameterRepresentation>();
        }
    }
}
