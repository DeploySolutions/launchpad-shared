using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.S3;
using Deploy.LaunchPad.AWS.SNS;
using Deploy.LaunchPad.AWS.SNS.Services;
using Deploy.LaunchPad.Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.AWS.SNS.Services
{
    public class AwsSnsService : SystemIntegrationServiceBase, IAwsSnsService
    {
        public virtual AmazonSimpleNotificationServiceClient SnsClient { get; protected set; }

        public virtual IAwsSnsHelper Helper { get; set; }

        public AwsSnsService(ILogger logger, IConfigurationRoot configurationRoot, Amazon.RegionEndpoint region) : base(logger,configurationRoot)
        {
            SnsClient = new AmazonSimpleNotificationServiceClient(region);
        }

        public AwsSnsService(ILogger logger, AmazonSimpleNotificationServiceClient snsClient) : base(logger)
        {
            SnsClient = snsClient;
        }


        public void SendNotification(string topicArn, string message)
        {

            var request = new PublishRequest
            {
                Message = message,
                TopicArn = topicArn
            };

            try
            {
                var response = SnsClient.PublishAsync(request);
                Logger.Info("Message sent to topic: " + message);
                Console.WriteLine("Message sent to topic:");
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Logger.Error("Caught exception publishing request: " + ex.Message);
                Console.WriteLine("Caught exception publishing request:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
