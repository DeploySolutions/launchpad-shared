using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Domain;

namespace DeploySoftware.LaunchPad.AWS.Redshift
{
    public interface IAwsRedshiftService : ILaunchPadDomainService
    {
        public IAwsRedshiftHelper Helper { get; set; }
    }
}
