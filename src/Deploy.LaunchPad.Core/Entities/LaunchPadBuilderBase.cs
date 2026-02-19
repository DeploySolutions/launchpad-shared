using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Entities
{
    /// <summary>
    /// Allows the creation of LaunchPad objects using a builder pattern.
    /// </summary>
    public abstract partial class LaunchPadBuilderBase : ILaunchPadBuilder
    {
        public abstract object Build();
    }
}
