using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    public partial interface ILikertScaleAnswer : IAnswerType
    {
        public string ToString();
    }
}
