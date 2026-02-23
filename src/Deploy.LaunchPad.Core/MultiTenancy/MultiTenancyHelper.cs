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

using System.Linq;
using System.Reflection;
using Deploy.LaunchPad.Core.Metadata;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Extensions;

namespace Deploy.LaunchPad.Core.MultiTenancy
{
    public partial class MultiTenancyHelper : HelperBase
    {
        public static bool IsMultiTenantEntity(object entity)
        {
            return entity is IMayHaveTenant || entity is IMustHaveTenant;
        }

        /// <param name="entity">The entity to check</param>
        /// <param name="expectedTenantId">TenantId or null for host</param>
        public static bool IsTenantEntity(object entity, int? expectedTenantId)
        {
            return (entity is IMayHaveTenant && entity.As<IMayHaveTenant>().TenantId == expectedTenantId) ||
                   (entity is IMustHaveTenant && entity.As<IMustHaveTenant>().TenantId == expectedTenantId);
        }

        public static bool IsHostEntity(object entity)
        {
            MultiTenancySideAttribute attribute = entity.GetType().GetTypeInfo()
                .GetCustomAttributes(typeof(MultiTenancySideAttribute), true)
                .Cast<MultiTenancySideAttribute>()
                .FirstOrDefault();

            if (attribute == null)
            {
                return false;
            }

            return attribute.Side.HasFlag(MultiTenancySides.Host);
        }
    }
}
