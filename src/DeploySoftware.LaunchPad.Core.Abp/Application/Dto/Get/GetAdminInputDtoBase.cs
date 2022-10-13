
using DeploySoftware.LaunchPad.Core.Application.Dto;

namespace DeploySoftware.LaunchPad.Core.Abp.Application.Dto
{
    public abstract partial class GetAdminInputDtoBase<TIdType> : GetFullInputDtoBase<TIdType>,
        ICanBeAppServiceMethodInput
    {
        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        public GetAdminInputDtoBase() : base()
        {

        }

        #endregion
    }
}
