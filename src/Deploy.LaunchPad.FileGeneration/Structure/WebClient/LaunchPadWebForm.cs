// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadWebForm.cs" company="Deploy Software Solutions, inc.">
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
    public partial class LaunchPadWebForm : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Domain entity representing the data in the form
        /// </summary>
        /// <value>The domain entity.</value>
        [JsonProperty("domainEntity")]
        public string DomainEntity { get; set; }

        /// <summary>
        /// Rows included in this form
        /// </summary>
        /// <value>The rows.</value>
        [JsonProperty("rows")]
        public IList<LaunchPadRow> Rows { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadWebForm"/> class.
        /// </summary>
        public LaunchPadWebForm() : base()
        {
            Rows = new List<LaunchPadRow>();
        }
    }
}
