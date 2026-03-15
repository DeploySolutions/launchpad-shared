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

using System.Globalization;

namespace Deploy.LaunchPad.Core.Localization
{
    /// <summary>
    /// Represents an available language for the application.
    /// </summary>
    public class LanguageInfo : ILanguageInfo
    {
        /// <summary>
        /// Code name of the language.
        /// It should be valid culture code.
        /// Ex: "en-US" for American English, "tr-TR" for Turkey Turkish.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the language in it's original language.
        /// Ex: "English" for English, "Türkçe" for Turkish.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// An icon can be set to display on the UI.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Is this the default language?
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Is this the language disabled?
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Is this language Right To Left?
        /// </summary>
        public bool IsRightToLeft
        {
            get
            {
                try
                {
                    return CultureInfo.GetCultureInfo(Name).TextInfo?.IsRightToLeft ?? false;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="LanguageInfo"/> object.
        /// </summary>
        /// <param name="name">
        /// Code name of the language.
        /// It should be valid culture code.
        /// Ex: "en-US" for American English, "tr-TR" for Turkey Turkish.
        /// </param>
        /// <param name="displayName">
        /// Display name of the language in it's original language.
        /// Ex: "English" for English, "Türkçe" for Turkish.
        /// </param>
        /// <param name="icon">An icon can be set to display on the UI</param>
        /// <param name="isDefault">Is this the default language?</param>
        /// <param name="isDisabled">Is this the language disabled?</param>
        public LanguageInfo(string name, string displayName, string icon = null, bool isDefault = false, bool isDisabled = false)
        {
            Name = name;
            DisplayName = displayName;
            Icon = icon;
            IsDefault = isDefault;
            IsDisabled = isDisabled;
        }
    }
}