using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public enum Authentication { Tenant, User, None }

    [Serializable]
    public partial class LaunchPadGeneratedConfiguration
    {
        /// <summary>
        /// The folder in which this item can be located, relative to its parent (LaunchPadGeneratedObject) object's folder.
        /// If it's empty, it is located in the same folder as its parent object.
        /// </summary>
        public string RelativeStartingPathFromParent { get; set; }

        /// <summary>
        /// The comma-delimited list of cultures this item can support
        /// </summary>
        public string SupportedCultures { get; set; }

        /// <summary>
        /// The version of this module
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// Authentication type:
        /// - tenant (default) = During the login, the user have an option to select which tenant they want to login to
        /// - user = Login without tenant selection
        /// - none = Does not require login
        /// </summary>
        public Authentication Authentication { get; set; }

        public virtual SourceControlRepository Repository { get; set; }

        public LaunchPadGeneratedConfiguration()
        {
            Repository = new SourceControlRepository();
            RelativeStartingPathFromParent = string.Empty;
            SupportedCultures = string.Empty; 
            Version = string.Empty;
            Authentication = Authentication.Tenant;
        }

    }
}
