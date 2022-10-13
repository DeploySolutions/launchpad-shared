using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Abp.Domain.Data
{
    public interface IFactSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey> : IDataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey>
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {

        public new IDictionary<TDictionaryKey, IFact<TDataPointPrimaryKey>> Data { get; set; }
    }
}
