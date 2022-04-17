using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public interface ISecretHelper
    {
        Task<IDictionary<string, string>> GetAllFieldsFromSecret(string secretVaultIdentifier);
        Task<string> GetDbConnectionStringFromSecretAsync(string secretVaultIdentifier, string connectionStringName);
        string GetDbConnectionStringFromSecret(string secretVaultIdentifier, string connectionStringName);

        string GetJsonFromSecret(string secretVaultIdentifier);
        Task<string> GetJsonFromSecretAsync(string secretVaultIdentifier);
        string GetValueFromSecret(string key, string secretVaultIdentifier);

        ISecretVault GetSecretVault(string secretVaultIdentifier, string name, string fullName); 
        Task<ISecretVault> GetSecretVaultAsync(string secretVaultIdentifier, string name, string fullName);

        Task<string> GetValueFromSecretAsync(string key, string secretVaultIdentifier);

        Task<IDictionary<string, string>> GetValuesFromSecret(IList<string> keys, string secretVaultIdentifier);

        string UpdateJsonForSecret(string secretVaultIdentifier, string originalSecretJson, string key, string value);
        HttpStatusCode WriteValuesToSecret(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier);
        Task<HttpStatusCode> WriteValuesToSecretAsync(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier);
    }
}