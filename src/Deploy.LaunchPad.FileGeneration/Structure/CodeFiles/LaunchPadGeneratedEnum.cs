﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-09-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedEnum.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a basic class generated by LaunchPad Framework.
    /// </summary>
    [Serializable]
    public partial class LaunchPadGeneratedEnum : LaunchPadGeneratedObjectBase
    {

        /// <summary>
        /// Contains information related to this object's position with a Visual Studio solution
        /// </summary>
        /// <value>The visual studio configuration.</value>
        public virtual VisualStudioClassConfiguration VisualStudioConfig { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        /// <value>The namespace.</value>
        public virtual string Namespace { get; set; }

        /// <summary>
        /// The class and interface inheritance of the enum.
        /// </summary>
        /// <value>The inherits from.</value>
        public virtual string InheritsFrom { get; set; } = "System.Int32";

        /// <summary>
        /// The using statements generated in this file.
        /// </summary>
        /// <value>The using statements.</value>
        public virtual string UsingStatements { get; set; }


        /// <summary>
        /// The access modifier/level of the class. Defaults to public.
        /// </summary>
        /// <value>The access modifier.</value>
        public LaunchPadGeneratedAccessModifier AccessModifier { get; set; }


        /// <summary>
        /// The dictionary of unique Enum Items that belong to this enum.
        /// </summary>
        /// <value>The items.</value>
        public virtual IDictionary<string, EnumItem> Items { get; set; }

        /// <summary>
        /// Where this file will be generated
        /// </summary>
        /// <value>The file path.</value>
        public virtual string FilePath { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedEnum"/> class.
        /// </summary>
        public LaunchPadGeneratedEnum() : base()
        {
            VisualStudioConfig = new VisualStudioClassConfiguration();
            AccessModifier = LaunchPadGeneratedAccessModifier.Public;
            Namespace = string.Empty;
            UsingStatements = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Items = new Dictionary<string, EnumItem>(comparer);
        }
    }

    /// <summary>
    /// Class EnumItem.
    /// </summary>
    public partial class EnumItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set;}

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public virtual int Value { get; set;}

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumItem"/> class.
        /// </summary>
        public EnumItem()
        {

        }
    }

}
