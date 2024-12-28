using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Config;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.CodeRepresentation
{
    public partial class ClassRepresentationGeneratorService: LaunchPadServiceBase, ILaunchPadService
    {

        public ClassRepresentationGeneratorService() : base()
        {
        }

        public ClassRepresentationGeneratorService(ILogger logger) : base(logger)
        {
        }

        public ClassRepresentationGeneratorService(ILogger logger, string name) : base(logger, name)
        {
        }

        public ClassRepresentationGeneratorService(ILogger logger, string name, string description) : base(logger, name, description)
        {
        }

        public virtual ClassRepresentation GenerateClassRepresentation<TDomain>(TDomain entity)
        {
            var classRepresentation = new ClassRepresentation(typeof(TDomain).Name);

            // Get properties
            var properties = typeof(TDomain).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var propertyRep = new PropertyRepresentation(property.Name, property.PropertyType);
                if (!property.PropertyType.IsEnum)
                {
                    propertyRep.PropertyType = ElementType.GetTypeInformationForElement(Logger, property.PropertyType.FullName, false);
                }
                classRepresentation.Properties.TryAdd(propertyRep.Name.Full, propertyRep);
            }

            // Get methods
            var methods = typeof(TDomain).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                         .Where(m => !m.IsSpecialName); // Exclude property accessors
            foreach (var method in methods)
            {
                var methodRep = new MethodRepresentation(method.Name, method.ReturnType);
                foreach (var parameter in method.GetParameters())
                {
                    ElementType paramType = ElementType.GetTypeInformationForElement(Logger, parameter.ParameterType.FullName, false);
                    var parameterRep = new MethodParameterRepresentation(parameter.Name, paramType);
                    methodRep.Parameters.Add(parameterRep);
                }
                classRepresentation.Methods.TryAdd(methodRep.Name.Full, methodRep);
            }

            return classRepresentation;
        }
    }
}
