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

namespace Deploy.LaunchPad.Util.MimeTypes
{
    public interface IMimeTypeMap
    {
        /// <summary>
        /// Tries to get the type of the MIME from the provided string.
        /// </summary>
        /// <param name="str">The filename or extension.</param>
        /// <param name="mimeType">The variable to store the MIME type.</param>
        /// <returns>Whether the transaction was completed successfully.</returns>
        /// <exception cref="ArgumentNullException" />
        bool TryGetMimeType(string str, out string mimeType);
        
        /// <summary>
        /// Gets the type of the MIME from the provided string.
        /// </summary>
        /// <param name="str">The filename or extension.</param>
        /// <param name="throwErrorIfNotFound">if set to <c>true</c>, throws error if extension's not found.</param>
        /// <returns>The MIME type.</returns>
        /// <exception cref="ArgumentNullException" />
        string GetMimeType(string str, bool throwErrorIfNotFound = true);
        
        /// <summary>
        /// Gets the extension from the provided MIME type.
        /// </summary>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="extension">The variable to store the extension.</param>
        /// <returns>Whether the transaction was completed successfully.</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        bool TryGetExtension(string mimeType, out string extension);
        
        /// <summary>
        /// Gets the extension from the provided MIME type.
        /// </summary>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="throwErrorIfNotFound">if set to <c>true</c>, throws error if extension's not found.</param>
        /// <returns>The extension.</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        string GetExtension(string mimeType, bool throwErrorIfNotFound = true);
        
        /// <summary>
        /// Adds MIME type to map
        /// </summary>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="extension">Type of the extension</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        void AddMimeType(string mimeType, string extension);
        
        /// <summary>
        /// Removes MIME type from map
        /// </summary>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        void RemoveMimeType(string mimeType);
        
        /// <summary>
        /// Adds extension to map
        /// </summary>
        /// <param name="extension">Type of the extension</param>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        void AddExtension(string extension, string mimeType);
        
        /// <summary>
        /// Removes extension from map
        /// </summary>
        /// <param name="extension">Type of the extension</param>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        void RemoveExtension(string extension);
    }
}