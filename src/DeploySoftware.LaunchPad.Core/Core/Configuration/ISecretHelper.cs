using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Configuration
{
    public interface ISecretHelper
    {
        Task<IDictionary<string, string>> GetAllFieldsFromSecret(string secretVaultIdentifier);
        Task<string> GetDbConnectionStringFromSecret(string secretVaultIdentifier);
        Task<string> GetJsonFromSecret(string secretVaultIdentifier);
        Task<string> GetValueFromSecret(string key, string secretVaultIdentifier);
        Task<IDictionary<string, string>> GetValuesFromSecret(IList<string> keys, string secretVaultIdentifier);
        string UpdateJsonForSecret(string secretVaultIdentifier, string originalSecretJson, string key, string value);
        HttpStatusCode WriteValuesToSecret(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier);
        Task<HttpStatusCode> WriteValuesToSecretAsync(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier);
    }
}