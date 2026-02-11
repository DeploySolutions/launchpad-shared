using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Domain.Questions
{
    [Serializable]
    public partial class DateTimeAnswer
    {
        public virtual DateTime Value { get; set; }
        public DateTimeAnswer(DateTime value) => Value = value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
