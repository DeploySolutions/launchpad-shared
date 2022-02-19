using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SQS.Services
{
    public partial class AwsSQSService : IAwsSQSService
    {
        public IAwsSQSHelper Helper { get; set; }
    }
}
