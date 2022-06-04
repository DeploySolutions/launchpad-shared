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

namespace DeploySoftware.LaunchPad.AWS.S3
{
    public partial class AwsS3HelperFactory : AwsHelperBase, ISingletonDependency
    {


        public virtual AwsS3Helper Create(
            ILogger logger,
            IConfigurationRoot _configurationRoot,
            string awsRegionEndpointName = DefaultRegionEndpointName, 
            string awsProfileName = DefaultLocalAwsProfileName, 
            bool shouldUseLocalAwsProfile = DefaultShouldUseLocalAwsProfile)
        {
            AwsS3Helper helper = null;
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            TryGetRegionEndpoint(awsRegionEndpointName, out RegionEndpoint region);
            Region = region;
            AwsProfileName = awsProfileName;
            if (shouldUseLocalAwsProfile)
            {
                var s3Client = GetS3Client(Region, AwsProfileName);
                helper = new AwsS3Helper(logger, _configurationRoot, awsRegionEndpointName, s3Client, AwsProfileName);
                helper.ShouldUseLocalAwsProfile = true;
            }
            else // do not use a named local profile, instead try to determine the AWS client from the credential resolution order
            {
                // attempt to load the registered instance, if any
                if (IocManager.Instance != null)
                {
                    try
                    {
                        helper = IocManager.Instance.Resolve<AwsS3Helper>();
                        logger.Debug("AwsS3Helper was registered; returning the resolved instance.");
                    }
                    catch (ComponentNotFoundException)
                    {
                        // create the helper using the AWS credentials resolution pattern.
                        // Since we are not using local profile here, we presumably load from EC2 role or environment
                        var s3Client = GetS3Client(Region);
                        helper = new AwsS3Helper(logger, _configurationRoot, awsRegionEndpointName, s3Client);
                        logger.Debug("AwsS3Helper was not registered; returning a new instance.");
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
                    var secretClient = GetS3Client(Region);
                    helper = new AwsS3Helper(logger, _configurationRoot, awsRegionEndpointName, secretClient);
                    logger.Debug("AwsS3Helper was null; returning a new instance.");
                }
            }
            
            return helper;
        }


        protected virtual AmazonS3Client GetS3Client(RegionEndpoint region, string profileName = "")
        {

            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_ProfileName, profileName));
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Region, profileName));

            AmazonS3Client client = null;
            try
            {
                if(!string.IsNullOrEmpty(profileName))
                {
                    var credentials = GetAwsCredentialsFromNamedLocalProfile(profileName);
                    if (credentials != null)
                    {
                        client = new AmazonS3Client(credentials, region);
                    }
                }
            }
            catch (AmazonSecretsManagerException smEx)
            {
                Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Exception_GetAwsCredentials, smEx.Message));
            }
            if (client == null) // try to load using local environment or EC2 information
            {
                client = new AmazonS3Client(region);
                Logger.Info(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_SecretClient_IsNull);
            }
            return client;

        }
    }
}
