using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial class LaunchPadAbpModuleSecretConfiguration : ILaunchPadAbpModuleSecretConfiguration
    {
        public virtual IDictionary<string, string> Fields { get; protected set; }
        public LaunchPadAbpModuleSecretConfiguration()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);
        }
    }
}
