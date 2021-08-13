using Castle.Core.Logging;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public abstract class HelperBase
    {
        public ILogger Logger { get; set; }

        protected HelperBase()
        {
            Logger = NullLogger.Instance;
        }

        protected HelperBase(ILogger logger)
        {
            Logger = logger;
        }


    }
}