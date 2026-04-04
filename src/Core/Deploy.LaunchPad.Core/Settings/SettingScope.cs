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

using System;

namespace Deploy.LaunchPad.Core.Configuration
{
    /// <summary>
    /// Defines scope of a setting.
    /// </summary>
    [Flags]
    public enum SettingScopes
    {
        /// <summary>
        /// Represents a placeholder setting that isn't relevent to scoping
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents a setting that can be configured/changed for the application level.
        /// </summary>
        Application = 1,

        /// <summary>
        /// Represents a setting that can be configured/changed for each Tenant.
        /// This is reserved
        /// </summary>
        Tenant = 2,

        /// <summary>
        /// Represents a setting that can be configured/changed for each User.
        /// </summary>
        User = 4,

        /// <summary>
        /// Represents a setting that can be configured/changed for all levels
        /// </summary>
        All = Application | Tenant | User
    }
}