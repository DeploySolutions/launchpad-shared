using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class NumericAnswer : AnswerBase
    {
        public virtual decimal Value { get; set; }

        public NumericAnswer(decimal value) => Value = value;

        public override string ToString()
        {
            return Value.ToString();
        }

    }
}
