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

using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Core
{
    public static class NamedTypeSelectorListExtensions
    {
        /// <summary>
        /// Add list of types to the list.
        /// </summary>
        /// <param name="list">List of NamedTypeSelector items</param>
        /// <param name="name">An arbitrary but unique name (can be later used to remove types from the list)</param>
        /// <param name="types"></param>
        public static void Add(this IList<NamedTypeSelector> list, string name, params Type[] types)
        {
            Check.NotNull(list, nameof(list));
            Check.NotNull(name, nameof(name));
            Check.NotNull(types, nameof(types));

            list.Add(new NamedTypeSelector(name, type => types.Any(type.IsAssignableFrom)));
        }
    }
}