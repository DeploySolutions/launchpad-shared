using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class LikertScaleTextualAnswer : AnswerBase, ILikertScaleAnswer
    {
        public virtual string Value { get; private set; }
        public virtual List<string> Scale { get; private set; }

        public LikertScaleTextualAnswer(List<string> scale, string value)
        {
            if (!scale.Contains(value))
                throw new ArgumentException("Value must be within the defined scale.");

            Scale = scale;
            Value = value;
        }

        public virtual void UpdateResponse(string newValue)
        {
            if (!Scale.Contains(newValue))
                throw new ArgumentException("New value must be within the defined scale.");

            Value = newValue;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
