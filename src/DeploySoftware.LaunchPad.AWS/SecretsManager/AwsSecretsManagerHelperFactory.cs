using Abp.Dependency;
using Amazon;
using Amazon.SecretsManager;
using Castle.Core.Logging;
using Castle.MicroKernel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public partial class AwsSecretsManagerHelperFactory : AwsHelperBase, ISingletonDependency
    {

        public AwsSecretsManagerHelperFactory() : base()
        {
        }

        public AwsSecretsManagerHelperFactory(ILogger logger, IConfigurationRoot configurationRoot, string awsRegionEndpointName) : base(logger, configurationRoot, awsRegionEndpointName)
        {

        }

        public virtual AwsSecretsManagerHelper Create(
            ILogger logger,
            IConfigurationRoot configurationRoot,
            string regionEndpointName = DefaultRegionEndpointName, 
            string localAwsProfileName = DefaultLocalAwsProfileName, 
            bool shouldUseLocalAwsProfile = DefaultShouldUseLocalAwsProfile)
        {
            Logger.Debug("AwsSecretsManagerHelperFactory.Create() started.");
            AwsSecretsManagerHelper helper = null;
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            TryGetRegionEndpoint(regionEndpointName, out RegionEndpoint region);
            Region = region;
            AwsProfileName = localAwsProfileName;
            ShouldUseLocalAwsProfile = shouldUseLocalAwsProfile;
            
            // attempt to load the registered instance, if any
            //if (IocManager.Instance != null)
            //{
            //    try
            //    {
            //        if(IocManager.Instance.IsRegistered<AwsSecretsManagerHelper>())
            //        {
            //            helper = IocManager.Instance.Resolve<AwsSecretsManagerHelper>();
            //            logger.Debug("AwsSecretsManagerHelper was registered; returning the resolved instance.");
            //        }
            //    }
            //    catch (ComponentNotFoundException)
            //    {
            //        logger.Debug("AwsSecretsManagerHelper claims it was registered but could not be resolved; returning a new instance.");
            //    }
            //}
            if(helper == null)
            {
                Logger.Debug("AwsSecretsManagerHelperFactory.Create() => helper was null, creating...");
                var secretClient = GetSecretClient(Region, AwsProfileName, ShouldUseLocalAwsProfile);
                helper = new AwsSecretsManagerHelper(logger, regionEndpointName, secretClient, AwsProfileName, ShouldUseLocalAwsProfile);
                Logger.Debug("AwsSecretsManagerHelperFactory.Create() => helper was null, created a new one.");
            }
            Logger.Debug("AwsSecretsManagerHelperFactory.Create() ended.");
            return helper;
        }


        protected virtual AmazonSecretsManagerClient GetSecretClient(RegionEndpoint region, string awsProfileName = "", bool shouldUseLocalAwsProfile = false)
        {
            Logger.Debug("AwsSecretsManagerHelperFactory.GetSecretClient() started.");
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_ProfileName, awsProfileName));
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Region, region.SystemName));

            AmazonSecretsManagerClient client = null;
            try
            {
                if(shouldUseLocalAwsProfile)
                {
                    if(string.IsNullOrEmpty(awsProfileName))
                    {
                        awsProfileName = DefaultLocalAwsProfileName;
                    }
                    var credentials = GetAwsCredentialsFromNamedLocalProfile(awsProfileName);
                    if (credentials != null)
                    {
                        client = new AmazonSecretsManagerClient(credentials, region);
                    }
                }
                else    
                {
                    client = new AmazonSecretsManagerClient(region);
                }
            }
            catch (AmazonSecretsManagerException smEx)
            {
                Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Exception_GetAwsCredentials, smEx.Message));
            }
            if (client == null) // try to load using local environment or EC2 information
            {
                client = new AmazonSecretsManagerClient(region);
                Logger.Info(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_SecretClient_IsNull);
            }
            Logger.Debug("AwsSecretsManagerHelperFactory.GetSecretClient() ended.");
            return client;

        }
    }
}
