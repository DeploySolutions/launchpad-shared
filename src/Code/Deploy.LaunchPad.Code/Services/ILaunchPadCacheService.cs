using Deploy.LaunchPad.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Services
{
    public partial interface ILaunchPadCacheService : IApplicationService
    {
        public Task ReloadAllAsync();

        public Task<Boolean> InvalidateCacheAsync();
    };
}
