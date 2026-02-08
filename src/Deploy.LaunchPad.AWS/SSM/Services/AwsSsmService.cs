
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.SSM.Services
{
    public class AwsSsmService : SystemIntegrationServiceBase, IAwsSsmService
    {
        public virtual AmazonSimpleSystemsManagementClient SsmClient { get; protected set; }

        public virtual IAwsSsmHelper Helper { get; set; }

        public AwsSsmService(ILogger logger, IConfigurationRoot configurationRoot, Amazon.RegionEndpoint region) : base(logger,configurationRoot)
        {
            SsmClient = new AmazonSimpleSystemsManagementClient(region);
        }

        public AwsSsmService(ILogger logger, AmazonSimpleSystemsManagementClient ssmClient) : base(logger)
        {
            SsmClient = ssmClient;
        }

        public async Task<string> GetParameterFromSystemsManager(string parameterName)
        {
            var value = string.Empty;
            var region = Amazon.RegionEndpoint.CACentral1;

            var request = new GetParameterRequest()
            {
                WithDecryption = true,
                Name = parameterName
            };

            using (var client = new AmazonSimpleSystemsManagementClient(region))
            {
                try
                {
                    var response = await client.GetParameterAsync(request);
                    value = response.Parameter.Value;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error occurred: {ex.Message}");
                }
            }
            return value;
        }
    }
}
