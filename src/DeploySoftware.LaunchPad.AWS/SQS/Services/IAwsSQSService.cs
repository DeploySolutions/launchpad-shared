using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Domain;

namespace DeploySoftware.LaunchPad.AWS.SQS.Services
{
    public interface IAwsSQSService : ILaunchPadDomainService
    {
        public IAwsSQSHelper Helper { get; set; }
    }
}
