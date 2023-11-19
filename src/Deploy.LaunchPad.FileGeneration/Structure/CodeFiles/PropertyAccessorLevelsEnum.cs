// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 04-20-2023
// ***********************************************************************
// <copyright file="PropertyAccessorLevelsEnum.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Enum PropertyAccessorLevelsEnum
    /// </summary>
    public enum PropertyAccessorLevelsEnum
    {
        /// <summary>
        /// The public
        /// </summary>
        Public = 0,
        /// <summary>
        /// The private
        /// </summary>
        Private = 1,
        /// <summary>
        /// The protected
        /// </summary>
        Protected = 2,
        /// <summary>
        /// The internal
        /// </summary>
        Internal = 3,
        /// <summary>
        /// The protected internal
        /// </summary>
        ProtectedInternal = 4
    }
}
