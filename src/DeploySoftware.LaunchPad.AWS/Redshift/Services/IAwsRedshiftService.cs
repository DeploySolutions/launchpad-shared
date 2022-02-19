using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Core.Configuration;

namespace DeploySoftware.LaunchPad.AWS.Redshift.Services
{
    public interface IAwsRedshiftService : ISystemIntegrationService
    {
        public IAwsRedshiftHelper Helper { get; set; }
    }
}
