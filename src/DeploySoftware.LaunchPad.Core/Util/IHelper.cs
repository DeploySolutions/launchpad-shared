using Abp.Dependency;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public interface IHelper : ISingletonDependency
    {

        public ILogger Logger { get; set; }
        public IConfigurationRoot ConfigurationRoot { get; }

        public string GetDescriptionFromEnum(Enum value, bool shouldReturnOriginalValueIfDescriptionEmpty = true);
    }
}
