﻿using ImageMagick;
using ImageMagick.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Images.Domain
{
    /// <summary>
    /// Configuration class for specifying the default settings of the ImageMagick utility
    /// </summary>
    public class ImageMagickConfiguration
    {
        public readonly string TemporaryImagesFilePath = @"d:\data\launchpad\images\temp"; 

        public ImageMagickConfiguration()
        {
            ConfigurationFiles configFiles = ConfigurationFiles.Default;
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

        public ImageMagickConfiguration(string policyMap, string temporaryImagesFilePath)
        {
            ConfigurationFiles configFiles = ConfigurationFiles.Default;
            configFiles.Policy.Data = policyMap;
            TemporaryImagesFilePath = temporaryImagesFilePath;
            MagickNET.Initialize(configFiles, TemporaryImagesFilePath);
        }
    }
}
