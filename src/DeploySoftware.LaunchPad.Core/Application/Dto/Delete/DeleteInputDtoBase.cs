
namespace DeploySoftware.LaunchPad.Core.Application
{
    public abstract partial class DeleteInputDtoBase<TIdType> : EntityDtoBase<TIdType>,
        ICanBeAppServiceMethodInput
    {

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected DeleteInputDtoBase() : base()
        {

        }

        #endregion
    }
}
