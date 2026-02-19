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
using System.Linq;
using System.Reflection;

namespace Deploy.LaunchPad.Util.Extensions
{
    public static class TypeExtensions
    {
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }

        public static MethodInfo GetMethod(this Type type, string methodName, int pParametersCount = 0, int pGenericArgumentsCount = 0)
        {
            return type
                .GetMethods()
                .Where(m => m.Name == methodName).ToList()
                .Select(m => new
                {
                    Method = m,
                    Params = m.GetParameters(),
                    Args = m.GetGenericArguments()
                })
                .Where(x => x.Params.Length == pParametersCount
                            && x.Args.Length == pGenericArgumentsCount
                ).Select(x => x.Method)
                .First();
        }

        /// <summary>
        /// Determines whether an instance of this type can be assigned to
        /// an instance of the <paramref name="targetType"></paramref>.
        ///
        /// Internally uses <see cref="Type.IsAssignableFrom"/> (as reverse).
        /// </summary>
        /// <param name="type">this type</param>
        /// <param name="targetType">Target type</param>
        public static bool IsAssignableTo(this Type type, Type targetType)
        {

            return targetType.IsAssignableFrom(type);
        }
    }
}
