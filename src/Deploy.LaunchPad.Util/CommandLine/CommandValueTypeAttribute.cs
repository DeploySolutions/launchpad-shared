using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FactoryLite.CommandLine
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CommandValueTypeAttribute : Attribute
    {
        public Type ValueType { get; }

        public CommandValueTypeAttribute(Type valueType)
        {
            ValueType = valueType;
        }
    }
}
