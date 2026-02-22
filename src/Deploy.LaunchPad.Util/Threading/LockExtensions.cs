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

namespace Deploy.LaunchPad.Util.Threading
{
    /// <summary>
    /// Extension methods to make locking easier.
    /// </summary>
    public static class LockExtensions
    {
        /// <summary>
        /// Executes given <paramref name="action"/> by locking given <paramref name="source"/> object.
        /// </summary>
        /// <param name="source">Source object (to be locked)</param>
        /// <param name="action">Action (to be executed)</param>
        public static void Locking(this object source, Action action)
        {
            lock (source)
            {
                action();
            }
        }

        /// <summary>
        /// Executes given <paramref name="action"/> by locking given <paramref name="source"/> object.
        /// </summary>
        /// <typeparam name="T">Type of the object (to be locked)</typeparam>
        /// <param name="source">Source object (to be locked)</param>
        /// <param name="action">Action (to be executed)</param>
        public static void Locking<T>(this T source, Action<T> action) where T : class
        {
            lock (source)
            {
                action(source);
            }
        }

        /// <summary>
        /// Executes given <paramref name="func"/> and returns it's value by locking given <paramref name="source"/> object.
        /// </summary>
        /// <typeparam name="TResult">Return type</typeparam>
        /// <param name="source">Source object (to be locked)</param>
        /// <param name="func">Function (to be executed)</param>
        /// <returns>Return value of the <paramref name="func"/></returns>
        public static TResult Locking<TResult>(this object source, Func<TResult> func)
        {
            lock (source)
            {
                return func();
            }
        }

        /// <summary>
        /// Executes given <paramref name="func"/> and returns it's value by locking given <paramref name="source"/> object.
        /// </summary>
        /// <typeparam name="T">Type of the object (to be locked)</typeparam>
        /// <typeparam name="TResult">Return type</typeparam>
        /// <param name="source">Source object (to be locked)</param>
        /// <param name="func">Function (to be executed)</param>
        /// <returns>Return value of the <paramnref name="func"/></returns>
        public static TResult Locking<T, TResult>(this T source, Func<T, TResult> func) where T : class
        {
            lock (source)
            {
                return func(source);
            }
        }
    }
}
