using Deploy.LaunchPad.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core
{
    public partial interface ILaunchPadCacheService : ILaunchPadService
    {
        public Task ReloadAllAsync();

        public Task<Boolean> InvalidateCacheAsync();
    };
}
