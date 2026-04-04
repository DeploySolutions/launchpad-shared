using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.CodeRepresentation
{
    /// <summary>
    /// This utility class assumes that the object type has a static CreateBuilder method that returns a builder object with a Build method.
    /// It's especially useful for dynamically creating Domain Entities at runtime but doesn't assume the Type is such.
    /// Warning: Passing in the wrong object type or arguments can result in runtime exceptions. Be especially careful of attempting to 
    /// instantiate classes with multiple constructors without specifying every argument, including optional ones. This is needed
    /// to ensure the correct constructor is called.
    /// </summary>
    public partial class LaunchPadBuilderInvoker
    {
        /// <summary>
        /// Builds an object of type T using the specified object type and arguments.
        /// </summary>
        /// <typeparam name="T">The type of object to build.</typeparam>
        /// <param name="objectType">The type of the object to build.</param>
        /// <param name="createBuilderArgs">The arguments to pass to the CreateBuilder method.</param>
        /// <returns>An instance of type T.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no matching CreateBuilder method is found or the Build method is missing.</exception>
        /// <exception cref="InvalidCastException">Thrown when the result of the Build method is not of type T.</exception>
        public T BuildObject<T>(Type objectType, Dictionary<string, object?> parameterMap)
        {
            // Get all CreateBuilder methods
            MethodInfo[] createBuilderMethods = objectType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => m.Name == "CreateBuilder")
                .ToArray();

            if (createBuilderMethods.Length == 0)
                throw new InvalidOperationException($"Type {objectType.Name} does not have any CreateBuilder methods.");

            MethodInfo? matchingMethod = null;
            object?[]? argsWithDefaults = null;

            // Iterate through all CreateBuilder methods to find a match
            foreach (var method in createBuilderMethods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                argsWithDefaults = new object?[parameters.Length];
                bool isMatch = true;

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameterMap.TryGetValue(parameters[i].Name!, out var value))
                    {
                        if (value == null && parameters[i].ParameterType.IsClass)
                        {
                            argsWithDefaults[i] = null;
                        }
                        else if (parameters[i].ParameterType.IsAssignableFrom(value?.GetType()))
                        {
                            argsWithDefaults[i] = value;
                        }
                        else
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    else if (parameters[i].IsOptional)
                    {
                        argsWithDefaults[i] = parameters[i].DefaultValue;
                    }
                    else if (parameters[i].ParameterType.IsClass || Nullable.GetUnderlyingType(parameters[i].ParameterType) != null)
                    {
                        argsWithDefaults[i] = null;
                    }
                    else
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                {
                    matchingMethod = method;
                    break;
                }
            }

            if (matchingMethod == null)
                throw new InvalidOperationException($"No matching CreateBuilder method found for type {objectType.Name} with the specified parameters.");

            // Call the matching CreateBuilder method with arguments
            object builder = matchingMethod.Invoke(null, argsWithDefaults) ?? throw new InvalidOperationException($"Failed to invoke CreateBuilder method on {objectType.Name}.");

            // Look for the Build method on the builder
            MethodInfo? buildMethod = builder.GetType().GetMethod("Build", BindingFlags.Public | BindingFlags.Instance);
            if (buildMethod == null)
                throw new InvalidOperationException($"Builder for {objectType.Name} does not have a Build method.");

            // Invoke the Build method to create the domain entity
            object result = buildMethod.Invoke(builder, null) ?? throw new InvalidOperationException($"Failed to invoke Build method on builder for {objectType.Name}.");

            // Ensure the result is of the expected type
            if (result is T typedResult)
            {
                return typedResult;
            }
            else
            {
                throw new InvalidCastException($"The result of {objectType.Name}.CreateBuilder.Build is not of type {typeof(T).Name}.");
            }
        }

    }
}
