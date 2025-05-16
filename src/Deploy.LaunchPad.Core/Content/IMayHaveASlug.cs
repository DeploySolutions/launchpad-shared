// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="ILaunchPadDomainEntityProperties.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Schema.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Content
{
    /// <summary>
    /// Allows a URL-like "slug" reference such as in WordPress my-content-slug.
    /// A slug is a human-readable, URL-safe string that uniquely identifies a resource — typically used in blog posts, articles, products, or anything accessible via URL. It's often derived from a title or name but formatted to be:
    /// -lowercase
    /// -hyphen-separated(-)
    /// -stripped of punctuation and special characters
    /// -ASCII-only(though some systems allow Unicode slugs)
    /// </summary>
    public partial interface IMayHaveASlug
    {
        [MaxLength(100)]
        [RegularExpression("^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Slug must be lowercase and hyphen-separated.")]
        public string? Slug { get; set; }

    }
}
