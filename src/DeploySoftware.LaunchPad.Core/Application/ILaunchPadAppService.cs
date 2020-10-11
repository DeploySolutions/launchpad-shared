using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Application
{
    /// <summary>
    /// This interface marks all LaunchPad App Services and ensures a consistent set of methods for manipulating the domain entities.
    /// </summary>
    public interface ILaunchPadAppService
    {

        Task<ICanBeAppServiceMethodOutput> Create(ICanBeAppServiceMethodInput input);

        Task<ICanBeAppServiceMethodOutput> Get(ICanBeAppServiceMethodInput input);

        Task<ICanBeAppServiceMethodOutput> GetDetail(ICanBeAppServiceMethodInput input);

        Task<ICanBeAppServiceMethodOutput> GetFull(ICanBeAppServiceMethodInput input);

        Task<ICanBeAppServiceMethodOutput> GetAdmin(ICanBeAppServiceMethodInput input);

        Task<ICanBeAppServiceMethodOutput> GetAll(ICanBeAppServiceMethodInput input);

        Task<ICanBeAppServiceMethodOutput> Update(ICanBeAppServiceMethodInput input);

        Task Delete(ICanBeAppServiceMethodInput input);

    }
}
