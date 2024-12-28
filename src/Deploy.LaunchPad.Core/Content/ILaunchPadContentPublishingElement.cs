using Deploy.LaunchPad.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Content
{
    public partial interface ILaunchPadContentPublishingElement : ILaunchPadCommonProperties
    {
        ContentElementType Type { get; set; }
    }
}
