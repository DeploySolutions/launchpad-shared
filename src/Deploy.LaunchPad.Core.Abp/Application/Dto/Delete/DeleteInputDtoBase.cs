


using Deploy.LaunchPad.Core.Application.Dto;

namespace Deploy.LaunchPad.Core.Abp.Application.Dto
{
    public abstract partial class DeleteInputDtoBase<TIdType> : EntityDtoBase<TIdType>, ICanBeAppServiceMethodInput
    {

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public DeleteInputDtoBase() : base()
        {

        }

        #endregion
    }
}
