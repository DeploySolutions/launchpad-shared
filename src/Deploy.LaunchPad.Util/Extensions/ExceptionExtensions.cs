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
using System.Runtime.ExceptionServices;

namespace Deploy.LaunchPad.Util.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Exception"/> class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Uses <see cref="ExceptionDispatchInfo.Capture"/> method to re-throws exception
        /// while preserving stack trace.
        /// </summary>
        /// <param name="exception">Exception to be re-thrown</param>
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}