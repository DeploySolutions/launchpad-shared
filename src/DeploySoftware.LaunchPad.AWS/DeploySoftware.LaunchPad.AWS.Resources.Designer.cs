﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeploySoftware.LaunchPad.AWS {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class DeploySoftware_LaunchPad_AWS_Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DeploySoftware_LaunchPad_AWS_Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DeploySoftware.LaunchPad.AWS.DeploySoftware.LaunchPad.AWS.Resources", typeof(DeploySoftware_LaunchPad_AWS_Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An exception was thrown while attempting to GetDbConnectionStringFromSecret for ARN: {0}. The message was {1}..
        /// </summary>
        public static string Logger_Error_GetDbConnectionStringFromSecret_ExceptionThrown {
            get {
                return ResourceManager.GetString("Logger_Error_GetDbConnectionStringFromSecret_ExceptionThrown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An exception was thrown while attempting to GetSecret JSON string: {0}. The message was {1}..
        /// </summary>
        public static string Logger_Error_GetJsonFromSecret_ExceptionThrown {
            get {
                return ResourceManager.GetString("Logger_Error_GetJsonFromSecret_ExceptionThrown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Getting credentials from Secrets Manager for secret ARN {0}.
        /// </summary>
        public static string Logger_Info_GetCredentialsFromSecret_Getting {
            get {
                return ResourceManager.GetString("Logger_Info_GetCredentialsFromSecret_Getting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Got credentials from Secrets Manager for secret ARN {0}.
        /// </summary>
        public static string Logger_Info_GetCredentialsFromSecret_Got {
            get {
                return ResourceManager.GetString("Logger_Info_GetCredentialsFromSecret_Got", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Getting DB Connection string from Secrets Manager for secret ARN {0}.
        /// </summary>
        public static string Logger_Info_GetDbConnectionStringFromSecret_Getting {
            get {
                return ResourceManager.GetString("Logger_Info_GetDbConnectionStringFromSecret_Getting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Got DB Connection string from Secrets Manager for secret ARN {0}.
        /// </summary>
        public static string Logger_Info_GetDbConnectionStringFromSecret_Got {
            get {
                return ResourceManager.GetString("Logger_Info_GetDbConnectionStringFromSecret_Got", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Getting JSON from Secrets Manager for secret ARN {0}.
        /// </summary>
        public static string Logger_Info_GetJsonFromSecret_Getting {
            get {
                return ResourceManager.GetString("Logger_Info_GetJsonFromSecret_Getting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Got JSON from Secrets Manager for secret ARN {0}.
        /// </summary>
        public static string Logger_Info_GetJsonFromSecret_Got {
            get {
                return ResourceManager.GetString("Logger_Info_GetJsonFromSecret_Got", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AWS region name is &apos;{0}&apos; (system name &apos;{1}&apos;)..
        /// </summary>
        public static string SecretHelper_GetRegionEndpoint_Logger_Info_RegionName {
            get {
                return ResourceManager.GetString("SecretHelper_GetRegionEndpoint_Logger_Info_RegionName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AWS Get Credentials failed. Message was {0}..
        /// </summary>
        public static string SecretHelper_GetSecretClient_Exception_GetAwsCredentials {
            get {
                return ResourceManager.GetString("SecretHelper_GetSecretClient_Exception_GetAwsCredentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AWS Profile name is {0}..
        /// </summary>
        public static string SecretHelper_GetSecretClient_ProfileName {
            get {
                return ResourceManager.GetString("SecretHelper_GetSecretClient_ProfileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AWS Region is {0}..
        /// </summary>
        public static string SecretHelper_GetSecretClient_Region {
            get {
                return ResourceManager.GetString("SecretHelper_GetSecretClient_Region", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AWS Secret Client was null, loaded from EC2 instance state..
        /// </summary>
        public static string SecretHelper_GetSecretClient_SecretClient_IsNull {
            get {
                return ResourceManager.GetString("SecretHelper_GetSecretClient_SecretClient_IsNull", resourceCulture);
            }
        }
    }
}
