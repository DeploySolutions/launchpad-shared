namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModule<TSecretHelper, TSecretVault, TSecretProvider>
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
        where TSecretProvider : SecretProviderBase<TSecretVault>, new()
    {
        
        public TSecretProvider SecretProvider { get; }

    }
}
