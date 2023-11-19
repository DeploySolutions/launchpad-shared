// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 04-20-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedMethodFieldBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Components.DictionaryAdapter;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using static System.Formats.Asn1.AsnWriter;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represent a C# method parameter or field
    /// </summary>
    [Serializable]
    public abstract partial class LaunchPadGeneratedMethodFieldBase : LaunchPadGeneratedObjectBase
    {

        /// <summary>
        /// Gets or sets the type of the item.
        /// </summary>
        /// <value>The type of the item.</value>
        public virtual LaunchPadGeneratedItemType ItemType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedMethodFieldBase"/> class.
        /// </summary>
        public LaunchPadGeneratedMethodFieldBase() : base()
        {
            ItemType = LaunchPadGeneratedItemType.Custom;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadGeneratedMethodFieldBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ItemType = (LaunchPadGeneratedItemType)info.GetValue("Id", typeof(LaunchPadGeneratedItemType));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ItemType", ItemType);
        }



    }
}
