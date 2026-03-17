using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Application.Connections
{
    public enum ConnectionType
    {
        None = 0,
        PostgresDatabase = 1,
        SqliteDatabase = 2,
        RestfulWebService = 3
    }
}
