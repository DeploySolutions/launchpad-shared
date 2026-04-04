using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class TimeStampAnswer : AnswerBase
    {
        public virtual TimeOnly Value { get; set; }
        public TimeStampAnswer(TimeOnly value) => Value = value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }

}
