// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Images
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ImageMagickConfiguration.cs" company="Deploy Software Solutions, inc.">
//     2016-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using ImageMagick;
using ImageMagick.Configuration;
using System.IO;

namespace Deploy.LaunchPad.Images.Domain
{
    /// <summary>
    /// Configuration class for specifying the default settings of the ImageMagick utility
    /// </summary>
    public partial class ImageMagickConfiguration
    {
        /// <summary>
        /// The temporary images file path
        /// </summary>
        public readonly string TemporaryImagesFilePath = @"d:\data\launchpad\images\temp";

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMagickConfiguration"/> class.
        /// </summary>
        public ImageMagickConfiguration()
        {
            IConfigurationFiles configFiles = ConfigurationFiles.Default;
            configFiles.Policy.Data = @"
           <policymap>
              <policy domain=""resource"" name=""temporary-path"" value=""/tmp""/>
              <policy domain=""resource"" name=""memory"" value=""2GiB""/> 
              <policy domain=""resource"" name=""map"" value=""4GiB""/> 
              <policy domain=""resource"" name=""area"" value=""1GB""/>
              <policy domain=""resource"" name=""disk"" value=""16EB""/>
              <policy domain=""resource"" name=""file"" value=""768""/>
              <policy domain=""resource"" name=""thread"" value=""4""/>
              <policy domain=""resource"" name=""throttle"" value=""0""/>
              <policy domain=""resource"" name=""time"" value=""unlimited""/> 
              <policy domain=""system"" name=""precision"" value=""6""/>
              <policy domain=""cache"" name=""shared-secret"" value=""passphrase""/>
              <policy domain=""coder"" rights=""none"" pattern=""EPHEMERAL"" />
              <policy domain=""coder"" rights=""none"" pattern=""URL"" />
              <policy domain=""coder"" rights=""none"" pattern=""HTTPS"" />
              <policy domain=""coder"" rights=""none"" pattern=""MVG"" />
              <policy domain=""coder"" rights=""none"" pattern=""MSL"" />
              <policy domain=""coder"" rights=""none"" pattern=""TEXT"" />
              <policy domain=""coder"" rights=""none"" pattern=""SHOW"" />
              <policy domain=""coder"" rights=""none"" pattern=""WIN"" />
              <policy domain=""coder"" rights=""none"" pattern=""PLT"" />
              <policy domain=""path"" rights=""none"" pattern=""@*"" />
            </policymap>
            ";
            MagickNET.Initialize(configFiles, TemporaryImagesFilePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMagickConfiguration"/> class.
        /// </summary>
        /// <param name="policyMap">The policy map.</param>
        /// <param name="temporaryImagesFilePath">The temporary images file path.</param>
        public ImageMagickConfiguration(string policyMap, string temporaryImagesFilePath)
        {
            IConfigurationFiles configFiles = ConfigurationFiles.Default;
            configFiles.Policy.Data = policyMap;
            TemporaryImagesFilePath = temporaryImagesFilePath;
            var mimePath = Path.Combine(temporaryImagesFilePath, "mime.xml");
            if (File.Exists(mimePath))
            {
                File.Delete(mimePath);
            }
            MagickNET.Initialize(configFiles, TemporaryImagesFilePath);
        }
    }
}
