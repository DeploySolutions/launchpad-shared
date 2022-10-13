using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Abp.Domain.Data
{
    /// <summary>
    /// Describes a dimension for data warehouse reporting. Facts have FK lookups to such dimensions.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    public interface IDimension<TPrimaryKey> : IDomainEntity<TPrimaryKey>, IMayHaveTenant
    {
    }
}
