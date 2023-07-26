using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Abp.Domain.Model;

namespace Deploy.LaunchPad.Core.Abp.Domain.Data
{
    /// <summary>
    /// Describes a dimension for data warehouse reporting. Facts have FK lookups to such dimensions.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    public interface IDimension<TPrimaryKey> : ILaunchPadDomainEntity<TPrimaryKey>, IMayHaveTenant
    {
    }
}
