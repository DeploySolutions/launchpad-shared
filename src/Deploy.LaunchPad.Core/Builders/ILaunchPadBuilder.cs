using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Builders
{
    /// <summary>
    /// Allows the creation of LaunchPad objects using a builder pattern.
    /// </summary>
    public partial interface ILaunchPadBuilder
    {
        object Build();
    }

    /// <summary>
    /// Allows the creation of strongly typed LaunchPad objects using a builder pattern.
    /// </summary>
    public partial interface ILaunchPadBuilder<T>
    {
        T Build();
    }
}
