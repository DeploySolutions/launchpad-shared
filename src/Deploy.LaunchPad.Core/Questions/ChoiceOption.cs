﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Questions
{
    [Serializable]
    public partial class ChoiceOption<T>
    {
        public virtual T Option { get; }
        public virtual string Label { get; }

        public ChoiceOption(T value, string label)
        {
            Option = value;
            Label = label;
        }

        public override string ToString() => $"{Option}: {Label}";
    }
}
