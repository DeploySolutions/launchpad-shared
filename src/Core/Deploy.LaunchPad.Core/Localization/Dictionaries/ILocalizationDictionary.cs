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

using System.Collections.Generic;
using System.Globalization;

namespace Deploy.LaunchPad.Core.Localization.Dictionaries
{
    /// <summary>
    /// Represents a dictionary that is used to find a localized string.
    /// </summary>
    public partial interface ILocalizationDictionary
    {
        /// <summary>
        /// Culture of the dictionary.
        /// </summary>
        CultureInfo CultureInfo { get; }

        /// <summary>
        /// Gets/sets a string for this dictionary with given name (key).
        /// </summary>
        /// <param name="name">Name to get/set</param>
        string this[string name] { get; set; }

        /// <summary>
        /// Gets a <see cref="string"/> for given <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Value to get key</param>
        /// <returns>The key or null</returns>
        string TryGetKey(string value);

        /// <summary>
        /// Gets a <see cref="ILocalizedString"/> for given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Name (key) to get localized string</param>
        /// <returns>The localized string or null if not found in this dictionary</returns>
        ILocalizedString GetOrNull(string name);

        /// <summary>
        /// Gets a <see cref="ILocalizedString"/> for given <paramref name="names"/>.
        /// </summary>
        /// <param name="names">Names (key) to get list of localized strings</param>
        /// <returns>The localized string or null if not found in this dictionary</returns>
        IReadOnlyList<ILocalizedString> GetStringsOrNull(List<string> names);

        /// <summary>
        /// Gets a list of all strings in this dictionary.
        /// </summary>
        /// <returns>List of all <see cref="ILocalizedString"/> object</returns>
        IReadOnlyList<ILocalizedString> GetAllStrings();
    }
}
