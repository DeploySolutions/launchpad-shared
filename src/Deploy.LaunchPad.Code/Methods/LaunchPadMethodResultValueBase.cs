using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Methods
{
    public abstract partial class LaunchPadMethodResultValueBase : ILaunchPadMethodResultValue
    {
        protected LaunchPadMethodResultValueBase()
        {
        }

        /// <summary>
        /// Creates an instance of the derived class from an existing instance of a compatible base class.
        /// </summary>
        /// <typeparam name="TInput">The type of the input value (must inherit from LaunchPadMethodResultValueBase).</typeparam>
        /// <typeparam name="TOutput">The type of the output value (must inherit from LaunchPadMethodResultValueBase).</typeparam>
        /// <param name="input">The input value to copy properties from.</param>
        /// <returns>A new instance of the output type with values copied from the input type.</returns>
        public static TOutput FromInput<TInput, TOutput>(TInput input)
            where TInput : LaunchPadMethodResultValueBase
            where TOutput : LaunchPadMethodResultValueBase
        {
            Guard.Against<ArgumentNullException>(input == null, "Input value cannot be null.");

            // Create a new instance of the output type using Activator.CreateInstance
            var output = Activator.CreateInstance<TOutput>();
            if (output == null)
            {
                throw new InvalidOperationException($"Failed to create an instance of type '{typeof(TOutput).Name}'.");
            }

            // Copy properties from the input to the output
            foreach (var property in typeof(TInput).GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(input);
                    var outputProperty = typeof(TOutput).GetProperty(property.Name);

                    if (outputProperty != null && outputProperty.CanWrite)
                    {
                        outputProperty.SetValue(output, value);
                    }
                }
            }

            // Validate that all required properties are set
            foreach (var property in typeof(TOutput).GetProperties())
            {
                if (property.GetCustomAttributes(typeof(System.Runtime.CompilerServices.RequiredMemberAttribute), inherit: true).Any())
                {
                    var value = property.GetValue(output);
                    if (value == null)
                    {
                        throw new InvalidOperationException($"The required property '{property.Name}' was not set during the creation of '{typeof(TOutput).Name}'.");
                    }
                }
            }

            return output;
        }
    }
}
