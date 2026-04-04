using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Methods
{
    // Event arguments for method events
    public partial class MethodEventArgs : EventArgs
    {
        public virtual string ClassName { get; }
        public virtual string MethodName { get; }
        public virtual object? Parameters { get; }

        public MethodEventArgs(string className, string methodName, object? parameters)
        {
            ClassName = className;
            MethodName = methodName;
            Parameters = parameters;
        }
    }
}
