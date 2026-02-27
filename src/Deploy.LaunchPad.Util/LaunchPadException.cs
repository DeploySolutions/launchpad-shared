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
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.Util
{
    /// <summary>
    /// Base exception type for those exceptions that are thrown by LaunchPad system for specific exceptions.
    /// </summary>
    [Serializable]
    public class LaunchPadException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="LaunchPadException"/> object.
        /// </summary>
        public LaunchPadException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="LaunchPadException"/> object.
        /// </summary>
        public LaunchPadException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="LaunchPadException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public LaunchPadException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="LaunchPadException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public LaunchPadException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
