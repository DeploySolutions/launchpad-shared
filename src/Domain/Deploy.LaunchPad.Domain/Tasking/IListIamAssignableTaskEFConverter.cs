using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Tasking
{
    public partial class IListIamAssignableTaskEFConverter<TInterface, TConcrete> : ValueConverter<IList<TInterface>, string>
        where TConcrete : class, TInterface
    {
        public IListIamAssignableTaskEFConverter()
        : base(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            v => (IList<TInterface>)JsonSerializer.Deserialize<List<TConcrete>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TInterface>()
        )
        { }
    }
}
