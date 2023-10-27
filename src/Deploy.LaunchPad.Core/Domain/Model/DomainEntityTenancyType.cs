using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    [Serializable]
    public enum DomainEntityTenancyType
    {
        None = 0,
        IMustHaveTenant = 1,
        IMayHaveTenant = 2
    }
}
