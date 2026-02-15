// SPDX-License-Identifier: Apache-2.0
//
// This file contains code originally from the ASP.NET Boilerplate - Web Application Framework project:
//   Repository: https://github.com/aspnetboilerplate/aspnetboilerplate
//   Original license: MIT License
//
// Modifications in this forked/copied version:
//   - Integrated into Deploy.LaunchPad.X (namespace adjustments)
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
using System.Linq;

namespace Deploy.LaunchPad.Util.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DayOfWeekExtensions"/>.
    /// </summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// Check if a given <see cref="DayOfWeek"/> value is weekend.
        /// </summary>
        public static bool IsWeekend(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        /// <summary>
        /// Check if a given <see cref="DayOfWeek"/> value is weekday.
        /// </summary>
        public static bool IsWeekday(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        }

        /// <summary>
        /// Finds the NTH week day of a month.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="n">The nth instance.</param>
        /// <remarks>Compensates for 4th and 5th DayOfWeek of Month</remarks>
        public static DateTime FindNthWeekDayOfMonth(this DayOfWeek dayOfWeek, int year, int month, int n)
        {
            if (n < 1 || n > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            var y = 0;

            var daysOfMonth = DateTimeExtensions.DaysOfMonth(year, month);

            // compensate for "last DayOfWeek in month"
            var totalInstances = dayOfWeek.TotalInstancesInMonth(year, month);
            if (n == 5 && n > totalInstances)
                n = 4;

            var foundDate = daysOfMonth
                .Where(date => dayOfWeek.Equals(date.DayOfWeek))
                .OrderBy(date => date)
                .Select(x => new { n = ++y, date = x })
                .Where(x => x.n.Equals(n)).Select(x => x.date).First(); //black magic wizardry

            return foundDate;
        }

        /// <summary>
        /// Finds the total number of instances of a specific DayOfWeek in a month.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        public static int TotalInstancesInMonth(this DayOfWeek dayOfWeek, int year, int month)
        {
            return DateTimeExtensions.DaysOfMonth(year, month).Count(date => dayOfWeek.Equals(date.DayOfWeek));
        }

        /// <summary>
        /// Gets the total number of instances of a specific DayOfWeek in a month.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="dateTime">The date in a month.</param>
        /// <returns></returns>
        public static int TotalInstancesInMonth(this DayOfWeek dayOfWeek, DateTime dateTime)
        {
            return dayOfWeek.TotalInstancesInMonth(dateTime.Year, dateTime.Month);
        }
    }
}