using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Deploy.LaunchPad.Util.Modules
{
    public partial interface ILaunchPadModule : ILaunchPadModuleInfo
    {
        ILogger Logger { get; set; }

        ILaunchPadModuleHelper Helper { get; set; }

        static abstract List<Type> FindDependedModuleTypes(Type moduleType);
        static abstract List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType);
        static abstract bool IsAbpModule(Type type);
        Assembly[] GetAdditionalAssemblies();
        void Initialize();
        void PostInitialize();
        void PreInitialize();
        void Shutdown();
    }
}