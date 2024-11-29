// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="TifWorldFile.cs" company="Deploy Software Solutions, inc.">
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


namespace Deploy.LaunchPad.Core.Files
{
    // Tiff World File (TFW)
    //
    /// <summary>
    /// Class TifWorldFile.
    /// Implements the <see cref="Model.DomainEntityFileBase{ System.Byte[]}" />
    /// </summary>
    /// <seealso cref="Model.DomainEntityFileBase{ System.Byte[]}" />
    public partial class TifWorldFile : FileBase<string>
    {
        /// <summary>
        /// The extension of the file
        /// </summary>
        /// <value>The extension.</value>
        public override string Extension { get => ".tfw"; }

        /// <summary>
        /// Line 1: A: x-scale
        /// </summary>
        /// <value>a.</value>
        public virtual decimal A { get; set; }

        /// <summary>
        /// Line 2: D: y-skew
        /// </summary>
        /// <value>The d.</value>
        public virtual decimal D { get; set; }

        /// <summary>
        /// Line 3: B: x-skew
        /// </summary>
        /// <value>The b.</value>
        public virtual decimal B { get; set; }

        /// <summary>
        /// Line 4: E: y-scale
        /// </summary>
        /// <value>The e.</value>
        public virtual decimal E { get; set; }

        /// <summary>
        /// Line 5: UTM easting of centre of upper-left pixel
        /// </summary>
        /// <value>The c.</value>
        public virtual decimal C { get; set; }

        /// <summary>
        /// Line 5: UTM northing of centre of upper-left pixel
        /// </summary>
        /// <value>The f.</value>
        public virtual decimal F { get; set; }

    }
}
