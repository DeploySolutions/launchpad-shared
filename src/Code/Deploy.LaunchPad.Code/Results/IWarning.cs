using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Results
{

    public partial interface IWarning : ISuccess
    {
        //
        // Summary:
        //     Reasons of the error
        public List<IWarning> Reasons { get; }
    }
}
