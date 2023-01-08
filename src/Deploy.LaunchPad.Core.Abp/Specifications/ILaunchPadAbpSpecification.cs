using Abp.Specifications;
using Deploy.LaunchPad.Core.Specifications;

namespace Deploy.LaunchPad.Core.Abp.Specifications
{
    public partial interface ILaunchPadAbpSpecification<T> :
        ILaunchPadSpecification<T>,
        ISpecification<T>
    {

    }
}
