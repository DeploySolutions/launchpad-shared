﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Core.Configuration;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager.Services
{
    public interface IAwsSecretsManagerService : ISystemIntegrationService
    {
        public IAwsSecretsManagerHelper Helper { get; set; }
    }
}