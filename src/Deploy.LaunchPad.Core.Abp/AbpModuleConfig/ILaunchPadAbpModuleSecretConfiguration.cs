using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleSecretConfiguration
    {
        public IDictionary<string, string> Fields { get; }
    }
}
