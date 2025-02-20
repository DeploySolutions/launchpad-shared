using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Questions
{
    [Serializable]
    public partial class TimeStampAnswer : AnswerBase
    {
        public virtual Timestamp Value { get; set; }
        public TimeStampAnswer(Timestamp value) => Value = value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }

}
