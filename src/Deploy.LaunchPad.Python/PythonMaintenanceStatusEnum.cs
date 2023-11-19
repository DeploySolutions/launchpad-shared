// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-11-2023
// ***********************************************************************
// <copyright file="PythonMaintenanceStatusEnum.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Enum PythonMaintenanceStatus
    /// </summary>
    public enum PythonMaintenanceStatus
    {
        /// <summary>
        /// The feature
        /// </summary>
        [Description("feature")] 
        Feature = 4, // new features, bugfixes, and security fixes are accepted.
        /// <summary>
        /// The prerelease
        /// </summary>
        [Description("prerelease")]
        Prerelease = 3, // feature fixes, bugfixes, and security fixes are accepted for the upcoming feature release.
        /// <summary>
        /// The security
        /// </summary>
        [Description("security")]
        Security = 2, // only security fixes are accepted and no more binaries are released, but new source-only versions can be released
        /// <summary>
        /// The end of life
        /// </summary>
        [Description("end-of-life")]
        EndOfLife = 1, // release cycle is frozen; no further changes can be pushed to it.
        /// <summary>
        /// The bug fix
        /// </summary>
        [Description("bugfix")]
        BugFix = 0, // bugfixes and security fixes are accepted, new binaries are still released. (Also called maintenance mode or stable release)

    }

}
