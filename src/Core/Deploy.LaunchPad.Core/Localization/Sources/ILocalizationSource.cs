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
using Deploy.LaunchPad.Util.Dependency;

namespace Deploy.LaunchPad.Core.Localization.Sources
{
    /// <summary>
    /// A Localization Source is used to obtain localized strings.
    /// </summary>
    public partial interface ILocalizationSource
    {
        /// <summary>
        /// Unique Name of the source.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// This method is called by ABP before first usage.
        /// </summary>
        void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver);

        /// <summary>
        /// Gets key for given value.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="culture">culture information</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// </param>
        /// <returns>Key</returns>
        string FindKeyOrNull(string value, CultureInfo culture, bool tryDefaults = true);

        /// <summary>
        /// Gets localized string for given name in current language.
        /// Fallbacks to default language if not found in current culture.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        string GetString(string name);

        /// <summary>
        /// Gets localized string for given name and specified culture.
        /// Fallbacks to default language if not found in given culture.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        string GetString(string name, CultureInfo culture);

        /// <summary>
        /// Gets localized string for given name in current language.
        /// Returns null if not found.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// </param>
        /// <returns>Localized string</returns>
        string GetStringOrNull(string name, bool tryDefaults = true);

        /// <summary>
        /// Gets localized string for given name and specified culture.
        /// Returns null if not found.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// </param>
        /// <returns>Localized string</returns>
        string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true);

        /// <summary>
        /// Gets list of localized strings for given names in current language.
        /// Fallbacks to default language if not found in current culture.
        /// </summary>
        /// <param name="names">Key names</param>
        /// <returns>Localized string</returns>
        List<string> GetStrings(List<string> names);

        /// <summary>
        /// Gets list of localized strings for given names and specified culture.
        /// Fallbacks to default language if not found in given culture.
        /// </summary>
        /// <param name="names">Key names</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        List<string> GetStrings(List<string> names, CultureInfo culture);

        /// <summary>
        /// Gets list of localized strings for given names  in current language.
        /// Returns null if not found.
        /// </summary>
        /// <param name="names">Key name</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// </param>
        /// <returns>Localized string</returns>
        List<string> GetStringsOrNull(List<string> names, bool tryDefaults = true);

        /// <summary>
        /// Gets list of localized strings for given names and specified culture.
        /// Returns null if not found.
        /// </summary>
        /// <param name="names">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="tryDefaults">
        /// True: Fallbacks to default language if not found in current culture.
        /// </param>
        /// <returns>Localized string</returns>
        List<string> GetStringsOrNull(List<string> names, CultureInfo culture, bool tryDefaults = true);

        /// <summary>
        /// Gets all strings in current language.
        /// </summary>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// </param>
        IReadOnlyList<ILocalizedString> GetAllStrings(bool includeDefaults = true);

        /// <summary>
        /// Gets all strings in specified culture.
        /// </summary>
        /// <param name="culture">culture information</param>
        /// <param name="includeDefaults">
        /// True: Fallbacks to default language texts if not found in current culture.
        /// </param>
        IReadOnlyList<ILocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true);
    }
}
