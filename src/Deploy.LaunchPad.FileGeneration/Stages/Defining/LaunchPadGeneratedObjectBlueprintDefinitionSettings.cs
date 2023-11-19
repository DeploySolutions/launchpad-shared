// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-05-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedObjectBlueprintDefinitionSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using Deploy.LaunchPad.FileGeneration.Structure;
using Deploy.LaunchPad.FileGeneration.Structure.SourceControl;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Enum Authentication
    /// </summary>
    public enum Authentication { Tenant, User, None }

    /// <summary>
    /// Class LaunchPadGeneratedObjectBlueprintDefinitionSettings.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Stages.ILaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Stages.ILaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    [Serializable]
    public partial class LaunchPadGeneratedObjectBlueprintDefinitionSettings : ILaunchPadGeneratedObjectBlueprintDefinitionSettings
    {

        /// <summary>
        /// The folder in which this item can be located, relative to its parent (LaunchPadGeneratedObject) object's folder.
        /// If it's empty, it is located in the same folder as its parent object.
        /// </summary>
        /// <value>The relative starting path from parent.</value>
        public virtual string RelativeStartingPathFromParent { get; set; }

        /// <summary>
        /// The comma-delimited list of cultures this item can support
        /// </summary>
        /// <value>The supported cultures.</value>
        public virtual string SupportedCultures { get; set; }

        /// <summary>
        /// The version of this blueprint
        /// </summary>
        /// <value>The version.</value>
        public virtual string Version { get; set; }

        /// <summary>
        /// Whether this element supports multi-tenancy
        /// </summary>
        /// <value><c>true</c> if [multi tenancy is enabled]; otherwise, <c>false</c>.</value>
        public virtual bool MultiTenancyIsEnabled { get; set; }

        /// <summary>
        /// Authentication type:
        /// - tenant (default) = During the login, the user have an option to select which tenant they want to login to
        /// - user = Login without tenant selection
        /// - none = Does not require login
        /// </summary>
        /// <value>The authentication.</value>
        public virtual Authentication Authentication { get; set; }


        // <summary>
        /// <summary>
        /// Contains a dictionary of file or folder exclusions paths that will be applied when assembling
        /// </summary>
        /// <value>The exclusion paths.</value>
        /// <font color="red">Badly formed XML comment.</font>
        public virtual IDictionary<string, string> ExclusionPaths { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings"/> class.
        /// </summary>
        public LaunchPadGeneratedObjectBlueprintDefinitionSettings()
        {
            RelativeStartingPathFromParent = string.Empty;
            SupportedCultures = string.Empty;
            Version = string.Empty;
            Authentication = Authentication.Tenant;
            var comparer = StringComparer.OrdinalIgnoreCase;
            ExclusionPaths = new Dictionary<string, string>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings"/> class.
        /// </summary>
        /// <param name="relativeStartPath">The relative start path.</param>
        public LaunchPadGeneratedObjectBlueprintDefinitionSettings(string relativeStartPath)
        {
            RelativeStartingPathFromParent = relativeStartPath;
            SupportedCultures = string.Empty;
            Version = string.Empty;
            Authentication = Authentication.Tenant;
            var comparer = StringComparer.OrdinalIgnoreCase;
            ExclusionPaths = new Dictionary<string, string>(comparer);
        }

    }
}
