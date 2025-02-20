using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Questions
{
    [Serializable]
    public partial class YesNoAnswer : AnswerBase
    {
        public virtual YesNoAnswerOption Value { get; set; }
        public YesNoAnswer(YesNoAnswerOption value) => Value = value;

        public override string ToString()
        {
            return Value switch
            {
                YesNoAnswerOption.Yes => "Yes",
                YesNoAnswerOption.No => "No",
                YesNoAnswerOption.NotApplicable => "Not Applicable",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}
