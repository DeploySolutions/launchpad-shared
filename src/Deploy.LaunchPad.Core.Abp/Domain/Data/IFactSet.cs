using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.Domain.Data
{
    public interface IFactSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey> : IDataSet<TPrimaryKey, TDictionaryKey, TDataPointPrimaryKey>
        where TDictionaryKey : struct
        where TDataPointPrimaryKey : struct
    {

        public new IDictionary<TDictionaryKey, IFact<TDataPointPrimaryKey>> Data { get; set; }
    }
}
