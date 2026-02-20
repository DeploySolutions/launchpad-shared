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

namespace Deploy.LaunchPad.Core.Application.Services.Dto
{
    /// <summary>
    /// Can be used to send/receive Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValueDto : NameValueDto<string>
    {
        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// </summary>
        public NameValueDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// </summary>
        public NameValueDto(string name, string value)
            : base(name, value)
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// </summary>
        /// <param name="nameValue">A <see cref="NameValue"/> object to get it's name and value</param>
        public NameValueDto(NameValue nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }

    /// <summary>
    /// Can be used to send/receive Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValueDto<T> : NameValue<T>
    {
        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// </summary>
        public NameValueDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// </summary>
        public NameValueDto(string name, T value)
            : base(name, value)
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// </summary>
        /// <param name="nameValue">A <see cref="NameValue"/> object to get it's name and value</param>
        public NameValueDto(NameValue<T> nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }
}