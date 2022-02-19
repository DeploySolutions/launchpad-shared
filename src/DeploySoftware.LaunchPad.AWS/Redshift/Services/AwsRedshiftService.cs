using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Redshift.Services
{
    public partial class AwsRedshiftService : IAwsRedshiftService
    {
        public IAwsRedshiftHelper Helper { get; set; }
    }
}
