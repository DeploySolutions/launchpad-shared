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

namespace Deploy.LaunchPad.Util.Specifications
{
    /// <summary>
    /// Represents that the implemented classes are specification parsers that
    /// parses the given specification to a domain specific criteria object, such 
    /// as the <c>ICriteria</c> instance in NHibernate.
    /// </summary>
    /// <typeparam name="TCriteria">The type of the domain specific criteria.</typeparam>
    public interface ISpecificationParser<TCriteria>
    {
        /// <summary>
        /// Parses the given specification to a domain specific criteria object.
        /// </summary>
        /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
        /// <param name="specification">The specified specification instance.</param>
        /// <returns>The instance of the domain specific criteria.</returns>
        TCriteria Parse<T>(ISpecification<T> specification);
    }
}
