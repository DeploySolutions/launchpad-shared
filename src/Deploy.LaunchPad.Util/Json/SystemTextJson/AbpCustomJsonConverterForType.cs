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
using System.Text.Json;
using System.Text.Json.Serialization;
using Deploy.LaunchPad.Util.Reflection;
using Deploy.LaunchPad.Util.Extensions;
using Deploy.LaunchPad.Util.Helpers;

namespace Deploy.LaunchPad.Util.Json.SystemTextJson
{
    public class AbpJsonConverterForType: JsonConverter<Type>
    {
        public override Type Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var assemblyQualifiedName = reader.GetString();
            if (assemblyQualifiedName.IsNullOrEmpty())
            {
                throw new Exception("AssemblyQualifiedName is empty!");
            }
            
            return Type.GetType(assemblyQualifiedName);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Type value,
            JsonSerializerOptions options
        )
        {
            var assemblyQualifiedName = TypeHelper.SerializeType(value).ToString();
            writer.WriteStringValue(assemblyQualifiedName);
        }
    }
}