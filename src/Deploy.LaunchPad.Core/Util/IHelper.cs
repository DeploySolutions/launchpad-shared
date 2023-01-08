using Castle.Core.Logging;
using System;

namespace Deploy.LaunchPad.Core.Util
{
    public interface IHelper
    {

        public ILogger Logger { get; set; }

        public string GetDescriptionFromEnum(Enum value, bool shouldReturnOriginalValueIfDescriptionEmpty = true);
    }
}
