﻿
using Abp.Domain.Entities;

namespace DeploySoftware.LaunchPad.Core.Application.Dto
{
    public abstract partial class GetInputDtoBase<TIdType> : EntityDtoBase<TIdType>,
        ICanBeAppServiceMethodInput
    {

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetInputDtoBase() : base()
        {

        }

        #endregion
    }
}