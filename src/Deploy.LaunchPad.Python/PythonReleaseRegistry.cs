// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Python
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 05-13-2023
// ***********************************************************************
// <copyright file="PythonReleaseRegistry.cs" company="Deploy Software Solutions, inc.">
//     2022-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Python
{
    /// <summary>
    /// Class PythonReleaseRegistry.
    /// Implements the <see cref="System.IEquatable{Deploy.LaunchPad.Python.PythonReleaseRegistry}" />
    /// </summary>
    /// <seealso cref="System.IEquatable{Deploy.LaunchPad.Python.PythonReleaseRegistry}" />
    [Serializable]
    public partial record PythonReleaseRegistry
    {
        /// <summary>
        /// Gets the releases.
        /// </summary>
        /// <value>The releases.</value>
        public virtual Dictionary<string, PythonVersion> Releases { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PythonReleaseRegistry"/> class.
        /// </summary>
        public PythonReleaseRegistry() { 
            Releases = PopulateReleases();
        }

        /// <summary>
        /// Populate (some) of the Python releases listed on https://www.python.org/
        /// </summary>
        /// <returns>Dictionary&lt;System.String, PythonVersion&gt;.</returns>
        protected Dictionary<string, PythonVersion> PopulateReleases()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var releases = new Dictionary<string, PythonVersion>(comparer);
            
            // 3.11.2
            var py311p2 = new PythonVersion(
                PythonMajorVersion.Three,
                PythonMinorVersion.Eleven, 
                2,
                new DateTime(2022, 10, 24),
                new DateTime(2027, 10, 24),
                PythonMaintenanceStatus.BugFix,
                new Uri("https://www.python.org/downloads/release/python-3112/"),
                new Uri("https://docs.python.org/release/3.11.2/whatsnew/changelog.html#python-3-11-2")
            );
            releases.Add(py311p2.GetFullVersion(), py311p2);

            // 3.11.1
            var py311p1 = new PythonVersion(
                PythonMajorVersion.Three,
                PythonMinorVersion.Eleven,
                1,
                new DateTime(2022, 12, 06),
                new DateTime(2027, 12, 6),
                PythonMaintenanceStatus.BugFix,
                new Uri("https://www.python.org/downloads/release/python-3111/"),
                new Uri("https://docs.python.org/release/3.11.1/whatsnew/changelog.html#python-3-11-1")
            );
            releases.Add(py311p1.GetFullVersion(), py311p1);

            // 3.10.10
            var py310p10 = new PythonVersion(
                PythonMajorVersion.Three,
                PythonMinorVersion.Ten,
                10,
                new DateTime(2023, 02, 8),
                new DateTime(2028, 02, 8),
                PythonMaintenanceStatus.BugFix,
                new Uri("https://www.python.org/downloads/release/python-31010/"),
                new Uri("https://docs.python.org/release/3.10.10/whatsnew/changelog.html#python-3-10-10-final")
            );
            releases.Add(py310p10.GetFullVersion(), py310p10);

            // 3.10.9
            var py310p9 = new PythonVersion(
                PythonMajorVersion.Three,
                PythonMinorVersion.Ten,
                9,
                new DateTime(2022, 12, 6),
                new DateTime(2027, 12, 6),
                PythonMaintenanceStatus.BugFix,
                new Uri("https://www.python.org/downloads/release/python-3109/"),
                new Uri("https://docs.python.org/release/3.10.9/whatsnew/changelog.html#python-3-10-9-final")
            );
            releases.Add(py310p9.GetFullVersion(), py310p9);

            // 3.9.16
            var py39p16 = new PythonVersion(
                PythonMajorVersion.Three,
                PythonMinorVersion.Nine,
                16,
                new DateTime(2022, 12, 6),
                new DateTime(2027, 12, 6),
                PythonMaintenanceStatus.Security,
                new Uri("https://www.python.org/downloads/release/python-3916/"),
                new Uri("http://docs.python.org/release/3.9.16/whatsnew/changelog.html")
            );
            releases.Add(py39p16.GetFullVersion(), py39p16);

            // 3.8.16
            var py38p16 = new PythonVersion(
                PythonMajorVersion.Three,
                PythonMinorVersion.Eight,
                16,
                new DateTime(2022, 12, 6),
                new DateTime(2027, 12, 6),
                PythonMaintenanceStatus.Security,
                new Uri("https://www.python.org/downloads/release/python-3816/"),
                new Uri("http://docs.python.org/release/3.8.16/whatsnew/changelog.html")
            );
            releases.Add(py38p16.GetFullVersion(), py38p16);

            // 3.7.16
            var py37p16 = new PythonVersion(
                PythonMajorVersion.Three,
                PythonMinorVersion.Seven,
                16,
                new DateTime(2022, 12, 6),
                new DateTime(2027, 12, 6),
                PythonMaintenanceStatus.Security,
                new Uri("https://www.python.org/downloads/release/python-3716/"),
                new Uri("https://docs.python.org/release/3.7.16/whatsnew/changelog.html#changelog")
            );
            releases.Add(py37p16.GetFullVersion(), py37p16);


            return releases;
        }
    }
}
