
namespace DeploySoftware.LaunchPad.Core.Application.Dto
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
