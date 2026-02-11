using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class DateOnlyAnswer : AnswerBase
    {
        public virtual DateOnly Value { get; set; }
        public DateOnlyAnswer(DateOnly value) => Value = value;

        public override string ToString()
        {
            return Value.ToString();
        }

    }
}
