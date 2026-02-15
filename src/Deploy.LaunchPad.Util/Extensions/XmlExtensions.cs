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
using System.Xml;

namespace Deply.LaunchPad.Util.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="XmlNode"/> class.
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Gets an attribute's value from an Xml node.
        /// </summary>
        /// <param name="node">The Xml node</param>
        /// <param name="attributeName">Attribute name</param>
        /// <returns>Value of the attribute</returns>
        public static string GetAttributeValueOrNull(this XmlNode node, string attributeName)
        {
            if (node.Attributes == null || node.Attributes.Count <= 0)
            {
                throw new Exception(node.Name + " node has not " + attributeName + " attribute");
            }

            return node.Attributes
                .Cast<XmlAttribute>()
                .Where(attr => attr.Name == attributeName)
                .Select(attr => attr.Value)
                .FirstOrDefault();
        }
    }
}
