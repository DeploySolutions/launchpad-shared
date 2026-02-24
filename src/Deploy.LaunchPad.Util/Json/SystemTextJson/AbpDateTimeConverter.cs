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
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Deploy.LaunchPad.Util.Extensions;
using Deploy.LaunchPad.Util.Dependency;
using Deploy.LaunchPad.Util.Timing;

namespace Deploy.LaunchPad.Util.Json.SystemTextJson
{
    public partial class AbpDateTimeConverter : JsonConverter<DateTime>
    {
        protected List<string> InputDateTimeFormats { get; set; }
        protected string OutputDateTimeFormat { get; set; }

        public AbpDateTimeConverter(List<string> inputDateTimeFormats = null, string outputDateTimeFormat = null)
        {
            InputDateTimeFormats = inputDateTimeFormats ?? new List<string>();
            OutputDateTimeFormat = outputDateTimeFormat;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!InputDateTimeFormats.IsNullOrEmpty())
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    foreach (var format in InputDateTimeFormats)
                    {
                        if (DateTime.TryParseExact(s, format, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var outDateTime))
                        {
                            return Clock.Normalize(outDateTime);
                        }
                    }
                }
                else
                {
                    throw new JsonException("Reader's TokenType is not String!");
                }
            }

            var dateText = reader.GetString();
            if (DateTime.TryParse(dateText, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var date))
            {
                return Clock.Normalize(date);    
            }

            throw new JsonException("Can't get datetime from the reader!");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (OutputDateTimeFormat.IsNullOrWhiteSpace())
            {
                writer.WriteStringValue(Clock.Normalize(value));
            }
            else
            {
                writer.WriteStringValue(Clock.Normalize(value).ToString(OutputDateTimeFormat, CultureInfo.CurrentUICulture));
            }
        }
    }

    public partial class AbpNullableDateTimeConverter : JsonConverter<DateTime?>, ITransientDependency
    {
        protected List<string> InputDateTimeFormats { get; set; }
        protected string OutputDateTimeFormat { get; set; }

        public AbpNullableDateTimeConverter(List<string> inputDateTimeFormats = null, string outputDateTimeFormat = null)
        {
            InputDateTimeFormats = inputDateTimeFormats ?? new List<string>();
            OutputDateTimeFormat = outputDateTimeFormat;
        }
        
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!InputDateTimeFormats.IsNullOrEmpty())
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    if (s.IsNullOrEmpty())
                    {
                        return null;
                    }
                    
                    foreach (var format in InputDateTimeFormats)
                    {
                        if (DateTime.TryParseExact(s, format, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var outDateTime))
                        {
                            return Clock.Normalize(outDateTime);
                        }
                    }
                }
                else
                {
                    throw new JsonException("Reader's TokenType is not String!");
                }
            }

            var dateText = reader.GetString();
            if (dateText.IsNullOrEmpty())
            {
                return null;
            }

            if (DateTime.TryParse(dateText, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var date))
            {
                return Clock.Normalize(date);    
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                if (OutputDateTimeFormat.IsNullOrWhiteSpace())
                {
                    writer.WriteStringValue(Clock.Normalize(value.Value));
                }
                else
                {
                    writer.WriteStringValue(Clock.Normalize(value.Value).ToString(OutputDateTimeFormat, CultureInfo.CurrentUICulture));
                }
            }
        }
    }
}
