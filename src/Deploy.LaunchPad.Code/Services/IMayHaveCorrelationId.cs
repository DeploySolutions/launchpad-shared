using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Services
{
    public partial interface IMayHaveCorrelationId
    {
        public Guid? CorrelationId { get; set; }
    }
}
