using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class TextualAnswer : AnswerBase
    {
        public virtual string Value { get; set; }
        public TextualAnswer(string value) => Value = value;

        public override string ToString()
        {
            return Value.ToString();
        }

    }
}
