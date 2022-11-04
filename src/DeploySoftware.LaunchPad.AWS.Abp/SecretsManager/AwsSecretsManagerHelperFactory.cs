using Abp.Dependency;
using Amazon;
using Amazon.SecretsManager;
using Castle.Core.Logging;
using Castle.MicroKernel;
using DeploySoftware.LaunchPad.AWS.SecretsManager;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.Abp.SecretsManager
{
    public partial class AwsSecretsManagerHelperFactory : AwsHelperBase<AmazonSecretsManagerConfig>, ISingletonDependency
    {

        public AwsSecretsManagerHelperFactory() : base()
        {
        }

        public AwsSecretsManagerHelperFactory(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }


        public virtual AwsSecretsManagerHelper Create(
            ILogger logger,
            string regionEndpointName = DefaultRegionEndpointName, 
            string localAwsProfileName = DefaultLocalAwsProfileName, 
            bool shouldUseLocalAwsProfile = DefaultShouldUseLocalAwsProfile,
            AwsClientSettings<AmazonSecretsManagerConfig> awsClientSettings = null
        )
        {
            Logger.Info("AwsSecretsManagerHelperFactory.Create() started.");
            AwsSecretsManagerHelper helper = null;
            if (logger == null)
            {
                logger = NullLogger.Instance;
            }
            TryGetRegionEndpoint(regionEndpointName, out RegionEndpoint region);
            Region = region;
            AwsProfileName = localAwsProfileName;
            ShouldUseLocalAwsProfile = shouldUseLocalAwsProfile;
            
            
            if(helper == null)
            {
                Logger.Debug("AwsSecretsManagerHelperFactory.Create() => helper was null, creating...");
                var secretClient = GetSecretClient(Region, AwsProfileName, ShouldUseLocalAwsProfile, awsClientSettings);
                helper = new AwsSecretsManagerHelper(logger, regionEndpointName, secretClient, AwsProfileName, ShouldUseLocalAwsProfile);
                Logger.Debug("AwsSecretsManagerHelperFactory.Create() => helper was null, created a new one.");
            }
            Logger.Info("AwsSecretsManagerHelperFactory.Create() ended.");
            return helper;
        }


        protected virtual AmazonSecretsManagerClient GetSecretClient(
            RegionEndpoint region, 
            string awsProfileName = "", 
            bool shouldUseLocalAwsProfile = false,
            AwsClientSettings<AmazonSecretsManagerConfig> awsClientSettings = null
        )
        {
            Logger.Info("AwsSecretsManagerHelperFactory.GetSecretClient() started.");
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_ProfileName, awsProfileName));
            Logger.Debug(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Region, region.SystemName));
            Logger.Debug(string.Format("shouldUseLocalAwsProfile?", shouldUseLocalAwsProfile));
            AmazonSecretsManagerClient client = null;
            try
            {
                if(shouldUseLocalAwsProfile)
                {
                    Logger.Debug("AwsSecretsManagerHelperFactory.GetSecretClient() => shouldUseLocalAwsProfile = true.");
                    if (string.IsNullOrEmpty(awsProfileName))
                    {
                        awsProfileName = DefaultLocalAwsProfileName;
                    }
                    var credentials = GetAwsCredentialsFromNamedLocalProfile(awsProfileName);
                    if (credentials != null)
                    {
                        Logger.Debug(string.Format(
                            "AwsSecretsManagerHelperFactory.GetSecretClient() => credentials was not null, creating client using local profile name '{0}'.",
                            awsProfileName
                        ));
                        AmazonSecretsManagerConfig clientConfig = new AmazonSecretsManagerConfig();
                        clientConfig.RegionEndpoint = region;
                        if(awsClientSettings != null) // use whatever the client settings are
                        {
                            clientConfig = awsClientSettings.Config;
                        }
                        client = new AmazonSecretsManagerClient(credentials, clientConfig);
                        
                        Logger.Debug("AwsSecretsManagerHelperFactory.GetSecretClient() => credentials was not null, created client.");
                    }
                }
                else    
                {
                    client = new AmazonSecretsManagerClient(region);
                }
            }
            catch (AmazonSecretsManagerException smEx)
            {
                Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Exception_GetAwsCredentials, smEx.Message));
            }
            if (client == null) // try to load using local environment or EC2 information
            {
                AmazonSecretsManagerConfig clientConfig = new AmazonSecretsManagerConfig();
                clientConfig.RegionEndpoint = region;
                if (awsClientSettings != null) // use whatever the client settings are
                {
                    clientConfig = awsClientSettings.Config;
                }
                client = new AmazonSecretsManagerClient(clientConfig);
                Logger.Info(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_SecretClient_IsNull);
            }
            Logger.Info("AwsSecretsManagerHelperFactory.GetSecretClient() ended.");
            return client;

        }
    }
}
