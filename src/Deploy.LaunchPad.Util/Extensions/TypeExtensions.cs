// SPDX-License-Identifier: Apache-2.0
//
// This file contains code originally from the ASP.NET Boilerplate - Web Application Framework project:
//   Repository: https://github.com/aspnetboilerplate/aspnetboilerplate
//   Original license: MIT License
//
// Modifications in this forked/copied version:
//   - Integrated into Deploy.LaunchPad.Core (namespace adjustments)
//   - Local fixes and adaptations (see git history for details)
//
// Original Copyright (c) Volosoft (https://volosoft.com) and contributors
// Modified files Copyright (c) Deploy Software Solutions, 2026
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// [EO License Header]


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
