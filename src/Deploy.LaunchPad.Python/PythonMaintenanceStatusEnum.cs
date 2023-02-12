using System.ComponentModel;

namespace Deploy.LaunchPad.Python
{
    public enum PythonMaintenanceStatus
    {
        [Description("feature")] 
        Feature = 4, // new features, bugfixes, and security fixes are accepted.
        [Description("prerelease")]
        Prerelease = 3, // feature fixes, bugfixes, and security fixes are accepted for the upcoming feature release.
        [Description("security")]
        Security = 2, // only security fixes are accepted and no more binaries are released, but new source-only versions can be released
        [Description("end-of-life")]
        EndOfLife = 1, // release cycle is frozen; no further changes can be pushed to it.
        [Description("bugfix")]
        BugFix = 0, // bugfixes and security fixes are accepted, new binaries are still released. (Also called maintenance mode or stable release)

    }

}
