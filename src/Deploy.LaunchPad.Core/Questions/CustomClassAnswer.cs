using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Questions
{
    [Serializable]
    public partial class CustomClassAnswer : AnswerBase
    {
        public virtual string CustomProperty { get; set; }
        public CustomClassAnswer(string customProperty) => CustomProperty = customProperty;
    }
}
