// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="PostBuildTextReplacement.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Class PostBuildTextReplacement.
    /// </summary>
    [Serializable]
    public partial class PostBuildTextReplacement
    {
        /// <summary>
        /// Gets or sets the original value.
        /// </summary>
        /// <value>The original value.</value>
        public virtual string OriginalValue { get; set; } = "";

        /// <summary>
        /// Gets or sets the replacement value.
        /// </summary>
        /// <value>The replacement value.</value>
        public virtual string ReplacementValue { get; set; } = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="PostBuildTextReplacement"/> class.
        /// </summary>
        public PostBuildTextReplacement()
        {

        }
    }
}
