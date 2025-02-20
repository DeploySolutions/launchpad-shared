using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Questions
{
    [Serializable]
    public partial class SingleChoiceAnswer<T> : IChoiceAnswer<T>
    {
        public virtual ChoiceOption<T> Value { get; private set; }
        public virtual List<ChoiceOption<T>> Options { get; }

        public SingleChoiceAnswer(List<ChoiceOption<T>> options, T value)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));

            Value = options.FirstOrDefault(opt => EqualityComparer<T>.Default.Equals(opt.Option, value))
                ?? throw new ArgumentException("Value must match one of the defined options.");
        }

        public void UpdateAnswer(T newValue)
        {
            Value = Options.FirstOrDefault(opt => EqualityComparer<T>.Default.Equals(opt.Option, newValue))
                ?? throw new ArgumentException("New value must match one of the defined options.");
        }

        public override string ToString()
        {
            return Value?.ToString() ?? "No response";
        }
    }
}
