using Castle.Core.Logging;
using Newtonsoft.Json;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public abstract class HelperBase : IHelper
    {
        protected ILogger _logger = NullLogger.Instance;
        protected HelperBase()
        {
        }

        protected HelperBase(ILogger logger)
        {
            _logger = logger;
        }


    }
}