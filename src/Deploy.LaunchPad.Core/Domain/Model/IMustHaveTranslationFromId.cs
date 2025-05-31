// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="IMustHaveTranslationFromId.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    /// <summary>
    /// Useful if data can be "translated" from one language to another.
    /// </summary>
    public partial interface IMustHaveTranslationFromId<TIdType>
    {
        [DataObjectField(false)]
        [DataMember(Name = "translatedFromId", EmitDefaultValue = false)]
        [XmlAttribute]
        public TIdType TranslatedFromId { get; set; }
    }
}
