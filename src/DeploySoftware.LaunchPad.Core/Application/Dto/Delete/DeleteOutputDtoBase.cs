
namespace DeploySoftware.LaunchPad.Core.Application.Dto
{
    public abstract partial class DeleteOutputDtoBase<TIdType> : ICanBeAppServiceMethodOutput
    {
        /// <summary>
        /// Determines if the delete operation succeeded. It is up to the implementer to determine what "success" means. 
        /// Defaults to false.
        /// </summary>
        public bool Succeeded { get; set; } = false;

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected DeleteOutputDtoBase() : base()
        {

        }

        #endregion
    }
}
