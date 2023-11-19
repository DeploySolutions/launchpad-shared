// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadDetailView.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a form with multiple input fields and action buttons.
    /// </summary>
    [Serializable]
    public partial class LaunchPadDetailView : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Domain entity representing the data in the view
        /// </summary>
        /// <value>The domain entity.</value>
        [JsonProperty("domainEntity")]
        public string DomainEntity { get; set; }

        /// <summary>
        /// Rows included in this view
        /// </summary>
        /// <value>The rows.</value>
        [JsonProperty("rows")]
        public IList<LaunchPadRow> Rows { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadDetailView"/> class.
        /// </summary>
        public LaunchPadDetailView() : base()
        {
            Rows = new List<LaunchPadRow>();
        }
    }
}
