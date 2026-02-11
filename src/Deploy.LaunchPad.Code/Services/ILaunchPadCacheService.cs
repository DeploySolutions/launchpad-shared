using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Services
{
    public partial interface ILaunchPadCacheService : ILaunchPadService
    {
        public Task ReloadAllAsync();

        public Task<Boolean> InvalidateCacheAsync();
    };
}
