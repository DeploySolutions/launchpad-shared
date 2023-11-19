// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-11-2023
// ***********************************************************************
// <copyright file="H3Helper.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using H3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.H3
{
    /// <summary>
    /// Class H3Helper.
    /// Implements the <see cref="HelperBase" />
    /// </summary>
    /// <seealso cref="HelperBase" />
    public partial class H3Helper : HelperBase
    {
        /// <summary>
        /// Creates the h3 index from hexadecimal.
        /// </summary>
        /// <param name="hex">The hexadecimal.</param>
        /// <returns>H3Index.</returns>
        public virtual H3Index CreateH3IndexFromHex(string hex)
        {
            H3Index index = new H3Index(hex);
            return index;

        }

        /// <summary>
        /// Creates the h3 index from u long.
        /// </summary>
        /// <param name="indexValue">The index value.</param>
        /// <returns>H3Index.</returns>
        public virtual H3Index CreateH3IndexFromULong(ulong indexValue)
        {
            H3Index index = new H3Index(indexValue);
            return index;
        }
    }
}
