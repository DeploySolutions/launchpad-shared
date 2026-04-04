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
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Deploy.LaunchPad.Util.Extensions;

namespace Deploy.LaunchPad.Util.Json.SystemTextJson
{
    public static partial class JsonSerializerOptionsHelper
    {
        public static JsonSerializerOptions Create(JsonSerializerOptions baseOptions, JsonConverter removeConverter, params JsonConverter[] addConverters)
        {
            return Create(baseOptions, x => x == removeConverter, addConverters);
        }

        public static JsonSerializerOptions Create(JsonSerializerOptions baseOptions, Func<JsonConverter, bool> removeConverterPredicate, params JsonConverter[] addConverters)
        {
            var options = new JsonSerializerOptions(baseOptions);

            var items = options.Converters.Where(removeConverterPredicate).ToList();
            foreach (var item in items)
            {
                options.Converters.Remove(item);
            }

            foreach (var jsonConverter in addConverters)
            {
                options.Converters.AddIfNotContains(jsonConverter);
            }
            return options;
        }
    }

}
