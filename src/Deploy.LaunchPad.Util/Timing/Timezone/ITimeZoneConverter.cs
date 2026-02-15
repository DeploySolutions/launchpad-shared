// SPDX-License-Identifier: Apache-2.0
//
// This file contains code originally from the ASP.NET Boilerplate - Web Application Framework project:
//   Repository: https://github.com/aspnetboilerplate/aspnetboilerplate
//   Original license: MIT License
//
// Modifications in this forked/copied version:
//   - Integrated into Deploy.LaunchPad.Core (namespace adjustments)
//   - Local fixes and adaptations (see git history for details)
//
// Original Copyright (c) Volosoft (https://volosoft.com) and contributors
// Modified files Copyright (c) Deploy Software Solutions, 2026
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// [EO License Header]


using System;

namespace Deploy.LaunchPad.Util.Timing.Timezone
{
    /// <summary>
    /// Interface for timezone converter
    /// </summary>
    public interface ITimeZoneConverter
    {
        /// <summary>
        /// Converts given date to user's time zone. 
        /// If timezone setting is not specified, returns given date.
        /// </summary>
        /// <param name="date">Base date to convert</param>
        /// <param name="tenantId">TenantId of user</param>
        /// <param name="userId">UserId to convert date for</param>
        /// <returns></returns>
        DateTime? Convert(DateTime? date, int? tenantId, long userId, string userTimeZone = "");

        /// <summary>
        /// Converts given date to tenant's time zone. 
        /// If timezone setting is not specified, returns given date.
        /// </summary>
        /// <param name="date">Base date to convert</param>
        /// <param name="tenantId">TenantId  to convert date for</param>
        /// <returns></returns>
        DateTime? Convert(DateTime? date, int tenantId, string tenantsTimeZone = "");

        /// <summary>
        /// Converts given date to application's time zone. 
        /// If timezone setting is not specified, returns given date.
        /// </summary>
        /// <param name="date">Base date to convert</param>
        /// <returns></returns>
        DateTime? Convert(DateTime? date, string applicationsTimeZone = "");
    }
}
