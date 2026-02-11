// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="IPhysicallyLocatable.cs" company="Deploy Software Solutions, inc.">
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


namespace Deploy.LaunchPad.Domain.Geospatial
{
    using System.Collections.Generic;

    /// <summary>
    /// This interface contracts that an object has a physical position that can be located in time and space
    /// </summary>
    public partial interface IMustBePhysicallyLocatable
    {
        /// <summary>
        /// The current physical location of the object in time and space
        /// </summary>
        /// <value>The current location.</value>
        SpaceTimeInformation CurrentLocation { get; set; }

        /// <summary>
        /// A list (not necessarily comprehensive) of this object's previous (but not current) physical positions.
        /// </summary>
        /// <value>The previous locations.</value>
        IList<SpaceTimeInformation> PreviousLocations { get; set; }

    }
}
