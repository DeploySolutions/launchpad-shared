using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class LikertScaleOptions
    {
        public virtual int Option { get; }
        public virtual string Label { get; }

        public LikertScaleOptions(int value, string label)
        {
            Option = value;
            Label = label;
        }

        public override string ToString() => $"{Option}: {Label}";
    }
}
