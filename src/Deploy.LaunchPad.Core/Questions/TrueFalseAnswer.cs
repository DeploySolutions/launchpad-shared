using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Questions
{
    [Serializable]
    public partial class TrueFalseAnswer : AnswerBase
    {
        public virtual bool Value { get; set; }
        public TrueFalseAnswer(bool value) => Value = value;

        public override string ToString()
        {
            return Value.ToString();
        }

    }
}