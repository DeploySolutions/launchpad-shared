// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IDateTimeProvider.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion

using System;

namespace Deploy.LaunchPad.Util
{
    /// <summary>
    /// This utility interface facilitates mocking against dates and times.
    /// </summary>
    public partial interface IDateTimeProvider
    {
        /// <summary>
        /// Returns the current date
        /// </summary>
        /// <value>The now.</value>
        DateTime Now { get; }

        /// <summary>
        /// Returns the current date, in UTC format
        /// </summary>
        /// <value>The UTC now.</value>
        DateTime UtcNow { get; }

        /// <summary>
        /// Returns a specified date object given a year, month, and day
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns>DateTime.</returns>
        DateTime SpecifyDate(int year, int month, int day);

    }
}
