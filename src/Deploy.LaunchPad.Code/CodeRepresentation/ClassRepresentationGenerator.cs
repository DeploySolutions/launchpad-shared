using Castle.Core.Logging;
using Deploy.LaunchPad.Code.Services;
using Deploy.LaunchPad.Code.Config;
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Deploy.LaunchPad.Util.Elements;
using Deploy.LaunchPad.Core.Application.Services;

namespace Deploy.LaunchPad.Code.CodeRepresentation
{
    public partial class ClassRepresentationGeneratorService: LaunchPadServiceBase, ILaunchPadService
    {

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public virtual ILogger Logger { get; set; } = NullLogger.Instance;

        public ClassRepresentationGeneratorService() : base()
        {
        }

        public ClassRepresentationGeneratorService(ILogger logger) : base()
        {
            Logger = logger;
        }

        public ClassRepresentationGeneratorService(ILogger logger, string name) : base( name)
        {
            Logger = logger;
        }

        public ClassRepresentationGeneratorService(ILogger logger, string name, string description) : base(name, description)
        {
            Logger = logger;
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
                classRepresentation.Properties.TryAdd(propertyRep.Name.Name, propertyRep);
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
                classRepresentation.Methods.TryAdd(methodRep.Name.Name, methodRep);
            }

            return classRepresentation;
        }
    }
}
