
namespace DeploySoftware.LaunchPad.Core.Application.Dto
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
