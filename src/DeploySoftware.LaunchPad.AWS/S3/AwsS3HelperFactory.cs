﻿using Abp.Dependency;
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
        public AwsS3HelperFactory(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }

        public virtual AwsS3Helper Create(
            ILogger logger,
            string awsRegionEndpointName = DefaultRegionEndpointName, 
            string awsProfileName = DefaultLocalAwsProfileName, 
            bool shouldUseLocalAwsProfile = DefaultShouldUseLocalAwsProfile)
        {
            
            AwsS3Helper helper = null;
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            logger.Debug("AwsS3HelperFactory.Create() => started.");

            logger.Debug("AwsS3HelperFactory.Create() => TryGetRegionEndpoint started.");
            TryGetRegionEndpoint(awsRegionEndpointName, out RegionEndpoint region);
            logger.Debug(string.Format("AwsS3HelperFactory.Create() => TryGetRegionEndpoint ended. Region is '{0}'.", region));
            
            Region = region;
            AwsProfileName = awsProfileName;
            if (shouldUseLocalAwsProfile)
            {
                logger.Debug("AwsS3HelperFactory.Create() => shouldUseLocalAwsProfile is true. GetS3Client started.");
                var s3Client = GetS3Client(Region, AwsProfileName);
                logger.Debug("AwsS3HelperFactory.Create() => shouldUseLocalAwsProfile is true. GetS3Client ended.");
                logger.Debug("AwsS3HelperFactory.Create() => shouldUseLocalAwsProfile is true. Creating new AwsS3Helper started.");

                helper = new AwsS3Helper(logger, awsRegionEndpointName, s3Client, AwsProfileName);
                helper.ShouldUseLocalAwsProfile = true;
                logger.Debug("AwsS3HelperFactory.Create() => shouldUseLocalAwsProfile is true. Creating new AwsS3Helper ended.");

            }
            else // do not use a named local profile, instead try to determine the AWS client from the credential resolution order
            {
                logger.Debug("AwsS3HelperFactory.Create() => shouldUseLocalAwsProfile is false.");

                // attempt to load the registered instance, if any
                if (IocManager.Instance != null)
                {
                    try
                    {
                        logger.Debug("AwsS3HelperFactory.Create() => IocManager.Instance != null, trying to resolve AwsS3Helper from IoC.");
                        helper = IocManager.Instance.Resolve<AwsS3Helper>();
                        logger.Debug("AwsS3HelperFactory.Create() => IocManager.Instance != null, resolved AwsS3Helper from IoC.");
                    }
                    catch (ComponentNotFoundException)
                    {
                        // create the helper using the AWS credentials resolution pattern.
                        // Since we are not using local profile here, we presumably load from EC2 role or environment
                        var s3Client = GetS3Client(Region);
                        helper = new AwsS3Helper(logger, awsRegionEndpointName, s3Client);
                        logger.Debug("AwsS3Helper was not registered; returning a new instance.");
                    }
                }
                else
                {

                    logger.Debug("AwsS3HelperFactory.Create() => IocManager.Instance was null.");
                }
                // after all that, load the helper
                if (helper == null)
                {
                    logger.Debug("AwsS3Helper was null; returning a new instance.");
                    // create the helper using the AWS credentials resolution pattern.
                    // Since we are not using local profile here, we presumably load from EC2 role or environment
                    var secretClient = GetS3Client(Region);
                    helper = new AwsS3Helper(logger, awsRegionEndpointName, secretClient);
                    logger.Debug("AwsS3Helper was null; returned a new instance.");
                }
            }
            logger.Debug("AwsS3HelperFactory.Create() => ended.");

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
