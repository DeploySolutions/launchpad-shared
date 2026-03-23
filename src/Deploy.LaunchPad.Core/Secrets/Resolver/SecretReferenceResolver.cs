using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Secrets.Configuration;
using Deploy.LaunchPad.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets.Resolver
{
    /// <summary>
    /// A class that resolves the value of a secret setting/field by looking up the setting's secret sources 
    /// (if any) and querying the appropriate secret vaults, in order, for the value of the setting.
    /// </summary>
    public partial class SecretReferenceResolver : ISecretReferenceResolver
    {

        public SecretReferenceResolver()
        {
        }

        public virtual ISecretResolutionResult TryResolve(IDictionary<string, ISettingDefinition> secrets, string fieldName)
        {
            Guard.AgainstNullOrWhiteSpace(fieldName, nameof(fieldName));
            ISecretResolutionResult result = null;
            if (secrets.ContainsKey(fieldName))
            {
                result = new SecretResolutionResult(fieldName, secrets[fieldName].DefaultValue);

            }
            else
            {
                result = new SecretResolutionResult(fieldName);
            }
            return result;
        }

        public virtual ISecretResolutionResult TryResolve(
            IDictionary<string, ISettingDefinition> secrets, ISettingDefinition settingDefinition)
        {
            Guard.AgainstNull(settingDefinition, nameof(settingDefinition));
            return TryResolve(secrets, settingDefinition.SecretReference.FieldName);
        }

    }
}
