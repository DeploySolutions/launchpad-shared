using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class LikertScaleNumericAnswer : AnswerBase, ILikertScaleAnswer
    {
        public virtual LikertScaleOptions Value { get; private set; }
        public virtual List<LikertScaleOptions> Scale { get; }

        public LikertScaleNumericAnswer(List<LikertScaleOptions> scale, int value)
        {
            Scale = scale ?? throw new ArgumentNullException(nameof(scale));

            var point = scale.FirstOrDefault(p => p.Option == value);
            if (point == null)
                throw new ArgumentException("Value must match one of the defined scale points.");

            Value = point;
        }

        public virtual void UpdateResponse(int newValue)
        {
            var point = Scale.FirstOrDefault(p => p.Option == newValue);
            if (point == null)
                throw new ArgumentException("New value must match one of the defined scale points.");

            Value = point;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
