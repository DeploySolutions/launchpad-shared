using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public partial class LaunchPadGeneratedModule<TModuleConfig,TComponent, TComponentConfig> : LaunchPadGeneratedObjectBase,
        ILaunchPadGeneratedModule<TModuleConfig,TComponent,TComponentConfig>
        where TModuleConfig: LaunchPadGeneratedConfiguration, new()
        where TComponent : LaunchPadGeneratedComponent<TComponentConfig>, new()
        where TComponentConfig : LaunchPadGeneratedConfiguration, new()
    {

        /// <summary>
        /// Contains information related to this object's configuration
        /// </summary>
        public virtual TModuleConfig Config { get; set; }

        /// <summary>
        /// The set of components that belong to this module.
        /// </summary>
        public virtual IDictionary<string, TComponent> Components { get; set; }


        public LaunchPadGeneratedModule() : base()
        {
            Config = new TModuleConfig();
            var comparer = StringComparer.OrdinalIgnoreCase;
            Components = new Dictionary<string, TComponent>(comparer);
        }
    }
}
