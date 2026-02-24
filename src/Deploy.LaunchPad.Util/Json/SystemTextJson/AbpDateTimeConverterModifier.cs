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
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Deploy.LaunchPad.Util.Reflection;
using Deploy.LaunchPad.Util.Timing;

namespace Deploy.LaunchPad.Util.Json.SystemTextJson
{
    public partial class AbpDateTimeConverterModifier
    {
        private readonly List<string> _inputDateTimeFormats;
        private readonly string _outputDateTimeFormat;

        public AbpDateTimeConverterModifier(List<string> inputDateTimeFormats, string outputDateTimeFormat)
        {
            _inputDateTimeFormats = inputDateTimeFormats;
            _outputDateTimeFormat = outputDateTimeFormat;
        }

        public Action<JsonTypeInfo> CreateModifyAction()
        {
            return Modify;
        }

        private void Modify(JsonTypeInfo jsonTypeInfo)
        {
            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(jsonTypeInfo.Type) != null)
            {
                return;
            }

            foreach (var property in jsonTypeInfo.Properties.Where(x => x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?)))
            {
                if (property.AttributeProvider == null ||
                    !property.AttributeProvider.GetCustomAttributes(typeof(DisableDateTimeNormalizationAttribute), false).Any())
                {
                    property.CustomConverter = property.PropertyType == typeof(DateTime)
                        ? (JsonConverter) new AbpDateTimeConverter(_inputDateTimeFormats, _outputDateTimeFormat)
                        : new AbpNullableDateTimeConverter(_inputDateTimeFormats, _outputDateTimeFormat);
                }
            }
        }
    }
}
