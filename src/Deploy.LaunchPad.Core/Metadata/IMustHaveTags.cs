// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="IMustHaveTags.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    /// <summary>
    /// Allows metadata tags to be stored
    /// </summary>
    public partial interface IMustHaveTags
    {
        /// <summary>
        /// Each item can have an open-ended set of tags applied to it, to help with data labelling or help users find, markup, and display its information
        /// </summary>
        /// <value>The tags.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Tags { get; }

    }
}
