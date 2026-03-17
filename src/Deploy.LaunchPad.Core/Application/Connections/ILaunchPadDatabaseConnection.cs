using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Application.Connections
{
    public partial interface ILaunchPadDatabaseConnection : ILaunchPadConnection
    {
        public string UserId { get;  }

        public string Password { get;  }    

        public string HostName { get;  }

        public int Port { get; }

        public string Version { get; }

        public string DatabaseName { get; }

        public string GetConnectionString();

    }
}
