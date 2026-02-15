using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class MultipleChoiceAnswer<T> : IChoiceAnswer<T>
    {
        public virtual List<ChoiceOptions<T>> SelectedValues { get; private set; }
        public virtual List<ChoiceOptions<T>> Options { get; }

        public MultipleChoiceAnswer(List<ChoiceOptions<T>> options, List<T> selectedValues)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));

            SelectedValues = selectedValues
                ?.Select(value => options.FirstOrDefault(opt => EqualityComparer<T>.Default.Equals(opt.Option, value))
                    ?? throw new ArgumentException("All selected values must be part of the defined options."))
                .ToList()
                ?? throw new ArgumentNullException(nameof(selectedValues));
        }

        public void UpdateAnswer(List<T> newValues)
        {
            SelectedValues = newValues
                ?.Select(value => Options.FirstOrDefault(opt => EqualityComparer<T>.Default.Equals(opt.Option, value))
                    ?? throw new ArgumentException("All new values must be part of the defined options."))
                .ToList();
        }

        public override string ToString()
        {
            return SelectedValues != null && SelectedValues.Any()
                ? string.Join(", ", SelectedValues)
                : "No response";
        }
    }
}
