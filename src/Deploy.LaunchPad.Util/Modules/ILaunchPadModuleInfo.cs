using System;
using System.Collections.Generic;
using System.Reflection;

namespace Deploy.LaunchPad.Util.Modules
{
    public partial interface ILaunchPadModuleInfo
    {
        /// <summary>
        /// Gets or sets the name of the internal module.
        /// </summary>
        /// <value>The name of the internal module.</value>
        public string InternalModuleName { get;  }

        Assembly Assembly { get; }
        List<ILaunchPadModuleInfo> Dependencies { get; }
        ILaunchPadModule Instance { get; }
        bool IsLoadedAsPlugIn { get; }
        Type Type { get; }

    }
}