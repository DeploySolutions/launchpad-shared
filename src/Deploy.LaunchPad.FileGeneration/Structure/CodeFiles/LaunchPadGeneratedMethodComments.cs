﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedMethodComments.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents the comments for a C# class method generated by LaunchPad Framework.
    /// </summary>
    [Serializable]
    public partial class LaunchPadGeneratedMethodComments
    {
        /// <summary>
        /// The summary text of the method comments.
        /// </summary>
        /// <value>The summary.</value>
        public virtual string Summary { get; set; } = string.Empty;

        /// <summary>
        /// The list of parameters, in order, that this method requires as input. If zero parameters are provided, this is a no-args method.
        /// The LaunchPadGeneratedMethodParameter "name" property will be the key and name of the param element,
        /// and the LaunchPadGeneratedMethodParameter description will be the text.
        /// </summary>
        public IDictionary<string, LaunchPadGeneratedMethodParameter> Parameters;

        /// <summary>
        /// The return text of the method comments (if any).
        /// </summary>
        /// <value>The returns.</value>
        public virtual string Returns { get; set; } = String.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedMethodComments"/> class.
        /// </summary>
        protected LaunchPadGeneratedMethodComments() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Parameters = new Dictionary<string, LaunchPadGeneratedMethodParameter>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedMethodComments"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public LaunchPadGeneratedMethodComments(IDictionary<string, LaunchPadGeneratedMethodParameter> parameters) : base()
        {
            if (parameters != null)
            {
                Parameters = parameters;
            }
            else
            {
                var comparer = StringComparer.OrdinalIgnoreCase;
                Parameters = new Dictionary<string, LaunchPadGeneratedMethodParameter>(comparer);
            }
        }
    }

}
