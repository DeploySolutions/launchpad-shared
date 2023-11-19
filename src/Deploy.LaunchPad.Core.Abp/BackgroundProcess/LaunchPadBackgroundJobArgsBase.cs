// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadBackgroundJobArgsBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.BackgroundProcess;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.BackgroundProcess
{
    /// <summary>
    /// Class LaunchPadBackgroundJobArgsBase.
    /// Implements the <see cref="ICanBeLaunchPadBackgroundJobArgs" />
    /// </summary>
    /// <seealso cref="ICanBeLaunchPadBackgroundJobArgs" />
    [Serializable]
    public abstract partial class LaunchPadBackgroundJobArgsBase : ICanBeLaunchPadBackgroundJobArgs
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Required]
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description short.
        /// </summary>
        /// <value>The description short.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DescriptionShort { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description full.
        /// </summary>
        /// <value>The description full.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string DescriptionFull { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the chron.
        /// </summary>
        /// <value>The chron.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Chron { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadBackgroundJobArgsBase"/> class.
        /// </summary>
        protected LaunchPadBackgroundJobArgsBase()
        {
            Id = Guid.NewGuid().ToString();
            Name = Id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadBackgroundJobArgsBase"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected LaunchPadBackgroundJobArgsBase(string id)
        {
            Id = id;
            Name = Id;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadBackgroundJobArgsBase"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="chron">The chron.</param>
        protected LaunchPadBackgroundJobArgsBase(string id, string name, string chron)
        {
            Id = id;
            Name = name;
            Chron = chron;
        }

    }
}
