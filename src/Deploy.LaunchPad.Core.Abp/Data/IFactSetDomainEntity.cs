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
using Deploy.LaunchPad.Domain.Data;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.Data
{
    /// <summary>
    /// Interface IFactSet
    /// Extends the <see cref="Abp.Domain.IDataSetDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TDictionaryKey">The type of the t dictionary key.</typeparam>
    /// <typeparam name="TDataPointPrimaryKey">The type of the t data point primary key.</typeparam>
    /// <typeparam name="TSchemaFormat">The type of the schema which can validate this entity.</typeparam>
    /// <seealso cref="Abp.Domain.IDataSetDomainEntity{TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey}" />
    public partial interface IFactSetDomainEntity<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey, TSchemaFormat> :
        ILaunchPadDataSet<TDictionaryKey, TSchemaFormat>,
        IDataSetDomainEntity<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey, TSchemaFormat>
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public new IDictionary<TDictionaryKey, IFactDomainEntity<TDataPointPrimaryKey, TSchemaFormat>> Data { get; set; }
    }
}
