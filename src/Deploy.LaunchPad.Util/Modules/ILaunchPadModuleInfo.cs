using System;
using System.Collections.Generic;
using System.Reflection;

namespace Deploy.LaunchPad.Util.Modules
{
    public interface ILaunchPadModuleInfo
    {
        Assembly Assembly { get; }
        List<ILaunchPadModuleInfo> Dependencies { get; }
        ILaunchPadModule Instance { get; }
        bool IsLoadedAsPlugIn { get; }
        Type Type { get; }

    }
}