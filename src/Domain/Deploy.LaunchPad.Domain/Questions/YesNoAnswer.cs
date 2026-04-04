using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class YesNoAnswer : AnswerBase
    {
        public virtual YesNoAnswerOptions Value { get; set; }
        public YesNoAnswer(YesNoAnswerOptions value) => Value = value;

        public override string ToString()
        {
            return Value switch
            {
                YesNoAnswerOptions.Yes => "Yes",
                YesNoAnswerOptions.No => "No",
                YesNoAnswerOptions.NotApplicable => "Not Applicable",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}
