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
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace Deploy.LaunchPad.Util.Resources.Embedded
{
    /// <summary>
    /// Stores needed information of an embedded resource.
    /// </summary>
    public partial class EmbeddedResourceItem
    {
        /// <summary>
        /// File name including extension.
        /// </summary>
        public virtual string FileName { get; }

        [CanBeNull]
        public virtual string FileExtension { get; }

        /// <summary>
        /// Content of the resource file.
        /// </summary>
        public virtual byte[] Content { get; set; }

        /// <summary>
        /// The assembly that contains the resource.
        /// </summary>
        public virtual Assembly Assembly { get; set; }

        public virtual DateTime LastModifiedUtc { get; }

        internal EmbeddedResourceItem(string fileName, byte[] content, Assembly assembly)
        {
            FileName = fileName;
            Content = content;
            Assembly = assembly;
            FileExtension = CalculateFileExtension(FileName);
            LastModifiedUtc = Assembly.Location != null
                ? new FileInfo(Assembly.Location).LastWriteTimeUtc
                : DateTime.UtcNow;
        }

        private static string CalculateFileExtension(string fileName)
        {
            if (!fileName.Contains("."))
            {
                return null;
            }

            return fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal) + 1);
        }
    }
}