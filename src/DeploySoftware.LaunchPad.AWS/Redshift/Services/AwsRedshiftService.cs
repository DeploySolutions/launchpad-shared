using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Redshift.Services
{
    public partial class AwsRedshiftService : SystemIntegrationServiceBase, IAwsRedshiftService
    {
        public IAwsRedshiftHelper Helper { get; set; }

        public AwsRedshiftService() : base()
        {
        }

        public AwsRedshiftService(ILogger logger) : base(logger)
        {

        }

    }
}
