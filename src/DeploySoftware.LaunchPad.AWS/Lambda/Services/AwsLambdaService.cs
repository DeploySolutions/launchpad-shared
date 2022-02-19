using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Lambda.Services
{
    public partial class AwsLambdaService : IAwsLambdaService
    {
        public IAwsLambdaHelper Helper { get; set; }
    }
}
