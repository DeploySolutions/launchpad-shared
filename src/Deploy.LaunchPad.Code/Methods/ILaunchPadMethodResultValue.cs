using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Methods
{
    public partial interface ILaunchPadMethodResultValue
    {
        /// <summary>
        /// Creates an instance of the derived class from an existing instance of a compatible base class.
        /// </summary>
        /// <typeparam name="TInput">The type of the input value (must inherit from LaunchPadMethodResultValueBase).</typeparam>
        /// <typeparam name="TOutput">The type of the output value (must inherit from LaunchPadMethodResultValueBase).</typeparam>
        /// <param name="input">The input value to copy properties from.</param>
        /// <returns>A new instance of the output type with values copied from the input type.</returns>
        public abstract static TOutput FromInput<TInput, TOutput>(TInput input)
            where TInput : LaunchPadMethodResultValueBase
            where TOutput : LaunchPadMethodResultValueBase
        ;
    }
}
