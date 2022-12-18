using Abp.Specifications;
using DeploySoftware.LaunchPad.Core.Specifications;

namespace DeploySoftware.LaunchPad.Core.Abp.Specifications
{
    public partial interface ILaunchPadAbpSpecification<T> :
        ILaunchPadSpecification<T>,
        ISpecification<T>
    {

    }
}
