using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    public partial interface IMustHaveIDProperty
    {
        // don't specify the actual property as if we inherit from ABP Domain Entity that will cause shadow property

    }

    public partial interface IHaveIDProperty<TIdType> : IMustHaveIDProperty
    {
       // don't specify the actual property as if we inherit from ABP Domain Entity that will cause shadow property

    }
}
