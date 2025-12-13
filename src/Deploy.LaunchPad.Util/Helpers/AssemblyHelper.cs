using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.Helpers
{
    public partial class AssemblyHelper : HelperBase
    {
        public AssemblyHelper() :base()
        {

        }

        public AssemblyHelper(ILogger logger) : base(logger)
        {

        }

        /// <summary>
        /// Gets the version string of the currently executing assembly.
        /// </summary>
        /// <returns></returns>
        public virtual string GetAssemblyVersionString()
        {
            return GetAssemblyVersionString<AssemblyHelper>();
        }

        /// <summary>
        /// Gets the version string of the assembly containing the specified type.
        /// </summary>
        /// <typeparam name="T">The type whose assembly version is to be retrieved.</typeparam>
        /// <returns>The version string of the assembly.</returns>
        public virtual string GetAssemblyVersionString<T>()
        {
            var asm = typeof(T).Assembly;
            var v = asm.GetName().Version;
            return v is null ? "0.0.0" : v.ToString();
        }
    }
}
