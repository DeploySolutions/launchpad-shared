using Castle.Core.Logging;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.CodeRepresentation
{
    public partial class PropertyRepresentation
    {
        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }
        public virtual ElementType PropertyType { get; set; }

        public PropertyRepresentation(string name, Type propertyType)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            PropertyType = ElementType.GetTypeInformationForElement(NullLogger.Instance, propertyType);
        }
    }

}
