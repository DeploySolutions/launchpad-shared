
namespace DeploySoftware.LaunchPad.Core.Application
{
    public abstract partial class GetAdminInputDtoBase<TIdType> : GetFullInputDtoBase<TIdType>, 
        ICanBeAppServiceMethodInput
    {
        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected GetAdminInputDtoBase() : base()
        {

        }

        #endregion
    }
}
