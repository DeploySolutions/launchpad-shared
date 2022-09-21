using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public interface ISecretHelper
    {
        Task<IDictionary<string, string>> GetAllFieldsFromSecret(string secretVaultIdentifier, string caller);
        Task<string> GetDbConnectionStringFromSecretAsync(string secretVaultIdentifier, string connectionStringName, string caller);
        string GetDbConnectionStringFromSecret(string secretVaultIdentifier, string connectionStringName, string caller);

        string GetJsonFromSecret(string secretVaultIdentifier, string caller);
        Task<string> GetJsonFromSecretAsync(string secretVaultIdentifier, string caller);
        string GetValueFromSecret(string key, string secretVaultIdentifier, string caller);

        ISecretVault GetSecretVault(string secretVaultIdentifier, string name, string fullName, string caller); 
        Task<ISecretVault> GetSecretVaultAsync(string secretVaultIdentifier, string name, string fullName, string caller);

        Task<string> GetValueFromSecretAsync(string key, string secretVaultIdentifier, string caller);

        Task<IDictionary<string, string>> GetValuesFromSecret(IList<string> keys, string secretVaultIdentifier, string caller);

        string UpdateJsonForSecret(string secretVaultIdentifier, string originalSecretJson, string key, string value, string caller);
        HttpStatusCode WriteValuesToSecret(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier, string caller);
        Task<HttpStatusCode> WriteValuesToSecretAsync(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier, string caller);
    }
}