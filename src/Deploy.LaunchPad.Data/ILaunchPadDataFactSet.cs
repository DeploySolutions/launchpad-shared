// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="IFactSet.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Util.Metadata;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Data
{
    /// <summary>
    /// Interface IFactSet
    /// Extends the <see cref="LaunchPad.Core.Abp.IDataSet{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TDictionaryKey">The type of the t dictionary key.</typeparam>
    /// <typeparam name="TDataPointPrimaryKey">The type of the t data point primary key.</typeparam>
    /// <seealso cref="LaunchPad.Core.Abp.IDataSet{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    public partial interface IFactSet<TDictionaryKey, TSchemaFormat> : ILaunchPadDataSet<TDictionaryKey, TSchemaFormat>
        where TDictionaryKey : struct
    {

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public new IDictionary<TDictionaryKey, ILaunchPadDataFact> Data { get; set; }
    }
}
