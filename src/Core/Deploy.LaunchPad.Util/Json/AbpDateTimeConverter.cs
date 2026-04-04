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
using System.Linq;
using System.Reflection;
using Deploy.LaunchPad.Util.Dependency;
using Deploy.LaunchPad.Util.Reflection;
using Deploy.LaunchPad.Util.Timing;
using Deploy.LaunchPad.Util.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Deploy.LaunchPad.Util.Json
{
    public partial class AbpDateTimeConverter : DateTimeConverterBase, ITransientDependency
    {
        private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
        private readonly DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
        private readonly CultureInfo _culture = CultureInfo.InvariantCulture;

        protected List<string> InputDateTimeFormats { get; set; }
        protected string OutputDateTimeFormat { get; set; }

        public AbpDateTimeConverter(List<string> inputDateTimeFormats = null, string outputDateTimeFormat = null)
        {
            InputDateTimeFormats = inputDateTimeFormats ?? new List<string>();
            OutputDateTimeFormat = outputDateTimeFormat;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var nullable = Nullable.GetUnderlyingType(objectType) != null;
            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw new JsonSerializationException($"Cannot convert null value to {objectType.FullName}.");
                }

                return null;
            }

            if (reader.TokenType == JsonToken.Date)
            {
                return Clock.Normalize(reader.Value.To<DateTime>());
            }

            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException($"Unexpected token parsing date. Expected String, got {reader.TokenType}.");
            }

            var dateText = reader.Value?.ToString();

            if (dateText.IsNullOrEmpty() && nullable)
            {
                return null;
            }

            if (InputDateTimeFormats.Any())
            {
                foreach (var format in InputDateTimeFormats)
                {
                    if (DateTime.TryParseExact(dateText, format, _culture, _dateTimeStyles, out var d1))
                    {
                        return Clock.Normalize(d1);
                    }
                }
            }

            var date = DateTime.Parse(dateText, _culture, _dateTimeStyles);

            return Clock.Normalize(date);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                value = Clock.Normalize(value.To<DateTime>());
            }

            if (value is DateTime dateTime)
            {
                if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal ||
                    (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
                {
                    dateTime = dateTime.ToUniversalTime();
                }

                writer.WriteValue(OutputDateTimeFormat.IsNullOrWhiteSpace()
                    ? dateTime.ToString(DefaultDateTimeFormat, _culture)
                    : dateTime.ToString(OutputDateTimeFormat, _culture));
            }
            else
            {
                throw new JsonSerializationException($"Unexpected value when converting date. Expected DateTime or DateTimeOffset, got {value.GetType()}.");
            }
        }

        public static bool ShouldNormalize(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType != typeof(DateTime) &&
                property.PropertyType != typeof(DateTime?))
            {
                return false;
            }

            return ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(member) == null;
        }
    }
}
