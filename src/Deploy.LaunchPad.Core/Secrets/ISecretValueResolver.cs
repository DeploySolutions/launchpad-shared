//using Deploy.LaunchPad.Core.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Deploy.LaunchPad.Core.Secrets
//{
//    public sealed class SecretResolveResult
//    {
//        public bool Found { get; init; }
//        public string? Value { get; init; }
//        public SecretSource? ResolvedBy { get; init; }
//        public string? ResolvedKey { get; init; }
//    }

//    public partial interface ISecretValueResolver
//    {
//        Task<SecretResolveResult> TryResolveAsync(
//            ISettingDefinition settingDefinition,
//            Guid? tenantId = null,
//            Guid? userId = null,
//            CancellationToken cancellationToken = default);

//        SecretResolveResult TryResolve(
//            ISettingDefinition settingDefinition,
//            Guid? tenantId = null,
//            Guid? userId = null);
//    }
//    public class SecretValueResolver : ISecretValueResolver
//    {
//        private readonly IReadOnlyDictionary<SecretSource, ISecretProvider> _providers;

//        public SecretValueResolver(IEnumerable<ISecretProvider> providers)
//        {
//            _providers = providers.ToDictionary(x => x.ProviderType);
//        }

//        public async Task<SecretResolveResult> TryResolveAsync(
//            ISettingDefinition settingDefinition,
//            Guid? tenantId = null,
//            Guid? userId = null,
//            CancellationToken cancellationToken = default)
//        {
//            if (settingDefinition.SecretSources == null || settingDefinition.SecretSources.Count == 0)
//            {
//                return new SecretResolveResult { Found = false };
//            }

//            foreach (var source in settingDefinition.SecretSources)
//            {
//                if (!_providers.TryGetValue(source.Provider, out var provider))
//                {
//                    continue;
//                }
//                ISecretVault vault = provider.GetSecretVaultById(source.VaultId, $"SecretValueResolver.TryResolveAsync for setting {settingDefinition.Name} for tenant {tenantId} and user {userId}");
//                var value = await vault.GetValueOrNullForSettingSecretProviderDescriptorAsync(
//                    source,
//                    settingDefinition,
//                    cancellationToken);

//                if (!string.IsNullOrEmpty(value))
//                {
//                    return new SecretResolveResult
//                    {
//                        Found = true,
//                        Value = value,
//                        ResolvedBy = source.Provider,
//                        ResolvedKey = source.Key
//                    };
//                }
//            }

//            return new SecretResolveResult { Found = false };
//        }

//        public SecretResolveResult TryResolve(
//            ISettingDefinition settingDefinition,
//            Guid? tenantId = null,
//            Guid? userId = null)
//        {
//            if (settingDefinition.SecretSources == null || settingDefinition.SecretSources.Count == 0)
//            {
//                return new SecretResolveResult { Found = false };
//            }

//            foreach (var source in settingDefinition.SecretSources)
//            {
//                if (!_providers.TryGetValue(source.Provider, out var provider))
//                {
//                    continue;
//                }

//                ISecretVault vault = provider.GetSecretVaultById(source.VaultId, $"SecretValueResolver.TryResolveAsync for setting {settingDefinition.Name} for tenant {tenantId} and user {userId}");
//                var value = vault.GetValueOrNullForSettingSecretProviderDescriptor(source, settingDefinition);

//                if (!string.IsNullOrEmpty(value))
//                {
//                    return new SecretResolveResult
//                    {
//                        Found = true,
//                        Value = value,
//                        ResolvedBy = source.Provider,
//                        ResolvedKey = source.Key
//                    };
//                }
//            }

//            return new SecretResolveResult { Found = false };
//        }
//    }
//}
