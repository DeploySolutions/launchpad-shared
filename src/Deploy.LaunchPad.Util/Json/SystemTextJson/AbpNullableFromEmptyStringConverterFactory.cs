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

using Deploy.LaunchPad.Util.Extensions;
using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Deploy.LaunchPad.Util.Json.SystemTextJson
{
    public class AbpNullableFromEmptyStringConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.GetTypeInfo().IsGenericType &&
                   typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(AbpNullableFromEmptyStringConverter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                null,
                culture: null);
        }
    }

    public class AbpNullableFromEmptyStringConverter<TNullableType> : JsonConverter<TNullableType>
    {
        private JsonSerializerOptions _readJsonSerializerOptions;
        private JsonSerializerOptions _writeJsonSerializerOptions;

        public override TNullableType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (_readJsonSerializerOptions == null)
            {
                _readJsonSerializerOptions = JsonSerializerOptionsHelper.Create(options, x =>
                    x == this ||
                    x.GetType() == typeof(AbpNullableFromEmptyStringConverterFactory));
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                if (reader.GetString().IsNullOrWhiteSpace())
                {
                    return default;
                }
            }

            return JsonSerializer.Deserialize<TNullableType>(ref reader, _readJsonSerializerOptions);
        }

        public override void Write(Utf8JsonWriter writer, TNullableType value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            if (_writeJsonSerializerOptions == null)
            {
                _writeJsonSerializerOptions = JsonSerializerOptionsHelper.Create(options, x =>
                    x == this ||
                    x.GetType() == typeof(AbpNullableFromEmptyStringConverterFactory));
            }

            var converter = (JsonConverter<TNullableType>) _writeJsonSerializerOptions.GetConverter(typeof(TNullableType));
            converter.Write(writer, value, _writeJsonSerializerOptions);
        }
    }
}
