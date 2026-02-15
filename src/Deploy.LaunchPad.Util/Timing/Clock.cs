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

namespace Deploy.LaunchPad.Util.Timing
{
    /// <summary>
    /// Used to perform some common date-time operations.
    /// </summary>
    public static class Clock
    {
        /// <summary>
        /// This object is used to perform all <see cref="Clock"/> operations.
        /// Default value: <see cref="UnspecifiedClockProvider"/>.
        /// </summary>
        public static IClockProvider Provider
        {
            get { return _provider; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Can not set Clock.Provider to null!");
                }

                _provider = value;
            }
        }

        private static IClockProvider _provider;

        static Clock()
        {
            Provider = ClockProviders.Unspecified;
        }

        /// <summary>
        /// Gets Now using current <see cref="Provider"/>.
        /// </summary>
        public static DateTime Now => Provider.Now;

        public static DateTimeKind Kind => Provider.Kind;

        /// <summary>
        /// Returns true if multiple timezone is supported, returns false if not.
        /// </summary>
        public static bool SupportsMultipleTimezone => Provider.SupportsMultipleTimezone;

        /// <summary>
        /// Normalizes given <see cref="DateTime"/> using current <see cref="Provider"/>.
        /// </summary>
        /// <param name="dateTime">DateTime to be normalized.</param>
        /// <returns>Normalized DateTime</returns>
        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }
    }
}