using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Results
{
    [Serializable]
    public partial class Warning : Success, IWarning
    {
        //
        // Summary:
        //     Reasons of the error
        public List<IWarning> Reasons { get; }

        protected Warning() : base()
        {
        }

        public Warning(string message) : base(message)
        {
        }
    }
}
