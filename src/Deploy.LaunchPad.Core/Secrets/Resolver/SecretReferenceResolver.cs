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
        private readonly ISecretConfiguration _secretConfiguration;

        public SecretReferenceResolver(ISecretConfiguration secretConfiguration)
        {
            _secretConfiguration = secretConfiguration;
        }

        public virtual ISecretResolutionResult TryResolve(string fieldName)
        {
            Guard.AgainstNullOrWhiteSpace(fieldName, nameof(fieldName));
            ISecretResolutionResult result = null;
            if (_secretConfiguration.Secrets.ContainsKey(fieldName))
            {
                result = new SecretResolutionResult(fieldName, _secretConfiguration.Secrets[fieldName].DefaultValue);

            }
            else
            {
                result = new SecretResolutionResult(fieldName);
            }
            return result;
        }

        public virtual ISecretResolutionResult TryResolve(
            ISettingDefinition settingDefinition)
        {
            Guard.AgainstNull(settingDefinition, nameof(settingDefinition));
            return TryResolve(settingDefinition.SecretReference.FieldName);
        }

    }
}
