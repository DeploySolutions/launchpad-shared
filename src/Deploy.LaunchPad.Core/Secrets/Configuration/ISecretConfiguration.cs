#region "Licensing"
/*
 * SPDX-FileCopyrightText: Copyright (c) Volosoft (https://volosoft.com) and contributors
 * SPDX-License-Identifier: MIT
 *
 * This file contains code originally from the ASP.NET Boilerplate - Web Application Framework:
 *   Repository: https://github.com/aspnetboilerplate/aspnetboilerplate
 *
 * The original portions of this file remain licensed under the MIT License.
 * You may obtain a copy of the MIT License at:
 *
 *   https://opensource.org/license/mit
 *
 *
 * SPDX-FileCopyrightText: Copyright (c) 2026 Deploy Software Solutions (https://www.deploy.solutions)
 * SPDX-License-Identifier: Apache-2.0
 *
 * Modifications and additional code in this file are licensed under
 * the Apache License, Version 2.0.
 *
 * You may obtain a copy of the Apache License at:
 *
 *   https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the Apache License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *
 * See the applicable license for governing permissions and limitations.
 */
#endregion

using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Util.Collections;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deploy.LaunchPad.Core.Secrets.Configuration
{
    public partial interface ISecretConfiguration
    {

        /// <summary>
        /// Secret provider.
        /// </summary>
        ISecretProvider Provider { get; init; }

        /// <summary>
        /// Contains a dictionary of "secret vaults", keyed using the friendly name of the vault.
        /// Each secret vault contains within it an inner dictionary of key value pairs representing a unique field contained within the vault, and the field's value.
        /// The secret vault also contains the unique identifer (such as Azure Key Vault identifier or AWS ARN) of the secret in which the secrets are stored.
        /// Note to implementers: Do not store or record this information!
        /// </summary>
        /// <value>The secret vaults.</value>
        [NotMapped]
        [JsonIgnore]
        public IDictionary<string, ISecretVault> Vaults { get; }

        [NotMapped]
        [JsonIgnore]
        public IDictionary<string, ISettingDefinition> Secrets { get; }

    }
}
