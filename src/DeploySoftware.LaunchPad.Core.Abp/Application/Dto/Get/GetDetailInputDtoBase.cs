using DeploySoftware.LaunchPad.Core.Application.Dto;

namespace DeploySoftware.LaunchPad.Core.Abp.Application.Dto
{
    public abstract partial class GetDetailInputDtoBase<TIdType> : GetInputDtoBase<TIdType>,
        ICanBeAppServiceMethodInput
    {
        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetDetailInputDtoBase() : base()
        {

        }

        #endregion
    }
}
