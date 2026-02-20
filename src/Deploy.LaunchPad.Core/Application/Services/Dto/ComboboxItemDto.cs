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
    /// This DTO can be used as a simple item for a combobox/list.
    /// </summary>
    [Serializable]
    public class ComboboxItemDto
    {
        /// <summary>
        /// Value of the item.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Display text of the item.
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Is selected?
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// </summary>
        public ComboboxItemDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// </summary>
        /// <param name="value">Value of the item</param>
        /// <param name="displayText">Display text of the item</param>
        public ComboboxItemDto(string value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}