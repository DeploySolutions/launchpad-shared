using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class IntegerAnswer : AnswerBase
    {
        public virtual long Value { get; set; }
        public IntegerAnswer(long value) => Value = value;

        public override string ToString()
        {
            return Value.ToString();
        }

    }
}
