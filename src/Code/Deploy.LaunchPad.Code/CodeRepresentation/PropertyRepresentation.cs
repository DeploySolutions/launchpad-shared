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
    public partial class PropertyRepresentation
    {
        public virtual ElementName Name { get; set; }
        public virtual ElementDescription Description { get; set; }
        public virtual ElementType PropertyType { get; set; }
        public virtual object? Value { get; set; }
        
        public PropertyRepresentation(string name, Type propertyType)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            PropertyType = ElementType.GetTypeInformationForElement(NullLogger.Instance, propertyType);
        }

        public PropertyRepresentation(string name, Type propertyType, object value)
        {
            Name = new ElementName(name);
            Description = new ElementDescription(name);
            PropertyType = ElementType.GetTypeInformationForElement(NullLogger.Instance, propertyType);
            Value = value;
        }
    }

}
