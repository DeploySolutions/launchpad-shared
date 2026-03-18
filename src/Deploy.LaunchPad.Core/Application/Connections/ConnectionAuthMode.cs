using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Application.Connections
{
    public enum ConnectionAuthMode
    {
        None = 0,
        UsernamePassword = 1,
        BearerToken = 2,
        ApiKey = 3,
        ConnectionString = 4
    }
}
