﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeploySoftware.LaunchPad.Images {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class DeploySoftware_LaunchPad_Images_Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DeploySoftware_LaunchPad_Images_Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DeploySoftware.LaunchPad.Images.DeploySoftware.LaunchPad.Images.Resources", typeof(DeploySoftware_LaunchPad_Images_Resources).Assembly);
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
        ///   Looks up a localized string similar to ImageMagick does not know how to compare these formats. More information may be found by searching online for MagickMissingDelegateErrorException &apos;no encode delegate for this image format&apos;.
        /// </summary>
        public static string Exception_ImageManager_CompareImages_MagickMissingDelegateErrorException {
            get {
                return ResourceManager.GetString("Exception_ImageManager_CompareImages_MagickMissingDelegateErrorException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Height must be greater than 0.
        /// </summary>
        public static string Guard_ImageManager_Thumbnail_Height_ArgumentOutOfRangeException {
            get {
                return ResourceManager.GetString("Guard_ImageManager_Thumbnail_Height_ArgumentOutOfRangeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image A is empty - byte size must be greater than 0.
        /// </summary>
        public static string Guard_ImageManager_Thumbnail_ImageA_ArgumentOutOfRangeException {
            get {
                return ResourceManager.GetString("Guard_ImageManager_Thumbnail_ImageA_ArgumentOutOfRangeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image A is null.
        /// </summary>
        public static string Guard_ImageManager_Thumbnail_ImageA_NullReferenceException {
            get {
                return ResourceManager.GetString("Guard_ImageManager_Thumbnail_ImageA_NullReferenceException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image B is empty - byte size must be greater than 0.
        /// </summary>
        public static string Guard_ImageManager_Thumbnail_ImageB_ArgumentOutOfRangeException {
            get {
                return ResourceManager.GetString("Guard_ImageManager_Thumbnail_ImageB_ArgumentOutOfRangeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image B is null.
        /// </summary>
        public static string Guard_ImageManager_Thumbnail_ImageB_NullReferenceException {
            get {
                return ResourceManager.GetString("Guard_ImageManager_Thumbnail_ImageB_NullReferenceException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Original image is empty - byte size must be greater than 0.
        /// </summary>
        public static string Guard_ImageManager_Thumbnail_OriginalImage_ArgumentOutOfRangeException {
            get {
                return ResourceManager.GetString("Guard_ImageManager_Thumbnail_OriginalImage_ArgumentOutOfRangeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Width must be greater than 0.
        /// </summary>
        public static string Guard_ImageManager_Thumbnail_Width_ArgumentOutOfRangeException {
            get {
                return ResourceManager.GetString("Guard_ImageManager_Thumbnail_Width_ArgumentOutOfRangeException", resourceCulture);
            }
        }
    }
}
