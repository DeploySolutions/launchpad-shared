using DeploySoftware.LaunchPad.Core.Application.Dto;

namespace DeploySoftware.LaunchPad.Core.Abp.Application.Dto
{
    public abstract partial class GetFullInputDtoBase<TIdType> : GetDetailInputDtoBase<TIdType>,
        ICanBeAppServiceMethodInput
    {
        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetFullInputDtoBase() : base()
        {

        }

        #endregion
    }
}
