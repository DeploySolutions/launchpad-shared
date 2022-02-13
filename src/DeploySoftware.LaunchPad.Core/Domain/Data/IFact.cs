using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Domain.Entities.Data
{
    /// <summary>
    /// Describes a fact (a "business event-based" data point) for data warehouse reporting purposes. 
    /// Facts often FK lookups to related dimensions which help with filtering and qualifying facts.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    public interface IFact<TPrimaryKey> : IDataPoint<TPrimaryKey>
    {
    }
}
