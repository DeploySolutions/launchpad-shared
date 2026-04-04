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
using System.Collections.Generic;
using System.Linq;
using Deploy.LaunchPad.Util.Timing;

namespace Deploy.LaunchPad.Util.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IDateTimeRange"/>.
    /// </summary>
    public static partial class DateTimeRangeExtensions
    {
        /// <summary>
        /// Sets date range to given target.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void SetTo(this IDateTimeRange source, IDateTimeRange target)
        {
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
        }

        /// <summary>
        /// Sets date range from given source.
        /// </summary>
        public static void SetFrom(this IDateTimeRange target, IDateTimeRange source)
        {
            target.StartTime = source.StartTime;
            target.EndTime = source.EndTime;
        }

        /// <summary>
        /// Returns all the days of a datetime range.
        /// </summary>
        /// <param name="dateRange">The date range.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysInRange(this IDateTimeRange dateRange)
        {
            return Enumerable.Range(0, (dateRange.TimeSpan).Days)
                .Select(offset => new DateTime(
                    dateRange.StartTime.AddDays(offset).Year,
                    dateRange.StartTime.AddDays(offset).Month,
                    dateRange.StartTime.AddDays(offset).Day));
        }

        /// <summary>
        /// Returns all the days in a range.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysInRange(DateTime start, DateTime end)
        {
            return new DateTimeRange(start, end).DaysInRange();
        }
    }
}