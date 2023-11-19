// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedDocumentTemplates.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class LaunchPadGeneratedDocumentTemplates.
    /// Implements the <see cref="TemplateBase" />
    /// </summary>
    /// <seealso cref="TemplateBase" />
    [Serializable]
    public partial class LaunchPadGeneratedDocumentTemplates : TemplateBase
    {
        /// <summary>
        /// Gets or sets the name of the header footer template.
        /// </summary>
        /// <value>The name of the header footer template.</value>
        public virtual string HeaderFooterTemplateName { get; set; }
        /// <summary>
        /// Gets or sets the cover page template names.
        /// </summary>
        /// <value>The cover page template names.</value>
        public virtual IList<string> CoverPageTemplateNames { get; set; }
        /// <summary>
        /// Gets or sets the closing page template names.
        /// </summary>
        /// <value>The closing page template names.</value>
        public virtual IList<string> ClosingPageTemplateNames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedDocumentTemplates"/> class.
        /// </summary>
        public LaunchPadGeneratedDocumentTemplates()
        {
            HeaderFooterTemplateName = string.Empty;
            CoverPageTemplateNames = new List<string>();
            ClosingPageTemplateNames = new List<string>();
        }
    }
}
