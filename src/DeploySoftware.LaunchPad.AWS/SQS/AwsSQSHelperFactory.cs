using Abp.Dependency;
using Amazon;
using Amazon.S3;
using Amazon.SecretsManager;
using Castle.Core.Logging;
using Castle.MicroKernel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SQS
{
    public partial class AwsSQSHelperFactory : AwsHelperBase, ISingletonDependency
    {

        public AwsSQSHelperFactory(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }

        public virtual AwsSQSHelper Create(
            ILogger logger,
            string awsRegionEndpointName = DefaultRegionEndpointName, 
            string awsProfileName = DefaultLocalAwsProfileName,             
            bool shouldUseLocalAwsProfile = DefaultShouldUseLocalAwsProfile)
        {
            AwsSQSHelper helper = null;
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            TryGetRegionEndpoint(awsRegionEndpointName, out RegionEndpoint region);
            Region = region;
            AwsProfileName = awsProfileName;
            if (shouldUseLocalAwsProfile)
            {
                helper = new AwsSQSHelper(logger, awsRegionEndpointName);
                helper.ShouldUseLocalAwsProfile = true;
            }
            else // do not use a named local profile, instead try to determine the AWS client from the credential resolution order
            {
                // attempt to load the registered instance, if any
                if (IocManager.Instance != null)
                {
                    try
                    {
                        helper = IocManager.Instance.Resolve<AwsSQSHelper>();
                        logger.Debug("AwsSQSHelper was registered; returning the resolved instance.");
                    }
                    catch (ComponentNotFoundException)
                    {
                        // create the helper using the AWS credentials resolution pattern.
                        // Since we are not using local profile here, we presumably load from EC2 role or environment
                        helper = new AwsSQSHelper(logger, awsRegionEndpointName);
                        logger.Debug("AwsSQSHelper was not registered; returning a new instance.");
                    }
                }
                else
                {

                }
                // after all that, load the helper
                if (helper == null)
                {
                    // create the helper using the AWS credentials resolution pattern.
                    // Since we are not using local profile here, we presumably load from EC2 role or environment
                    helper = new AwsSQSHelper(logger, awsRegionEndpointName);
                    logger.Debug("AwsSQSHelper was null; returning a new instance.");
                }
            }
            
            return helper;
        }

    }
}
