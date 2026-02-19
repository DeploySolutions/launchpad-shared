// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Images
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ThumbnailGenerator.cs" company="Deploy Software Solutions, inc.">
//     2016-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion


using Castle.Core.Logging;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Domain;
using ImageMagick;
using System;
using System.IO;
using System.Reflection;
using Deploy.LaunchPad.Code.Services;
using Deploy.LaunchPad.Core.Elements;
using Deploy.LaunchPad.Core.Entities;

namespace Deploy.LaunchPad.Images.Domain
{

    /// <summary>
    /// This class creates thumbnails from provided images, using Magick.NET library (ImageMagick wrapper)
    /// </summary>
    public partial class ThumbnailGenerator : SystemIntegrationServiceBase
    {
        /// <summary>
        /// The configuration
        /// </summary>
        public readonly ImageMagickConfiguration Configuration;

        /// <summary>
        /// The width of the small thumbnail format, in pixels
        /// </summary>
        /// <value>The width of the thumbnail small.</value>
        public int ThumbnailSmallWidth { get; set; } = 100;

        /// <summary>
        /// The height of the small thumbnail format, in pixels
        /// </summary>
        /// <value>The height of the thumbnail small.</value>
        public int ThumbnailSmallHeight { get; set; } = 100;

        /// <summary>
        /// The width of the medium thumbnail format, in pixels
        /// </summary>
        /// <value>The width of the thumbnail medium.</value>
        public int ThumbnailMediumWidth { get; set; } = 300;

        /// <summary>
        /// The height of the medium thumbnail format, in pixels
        /// </summary>
        /// <value>The height of the thumbnail medium.</value>
        public int ThumbnailMediumHeight { get; set; } = 300;

        /// <summary>
        /// The width of the large thumbnail format, in pixels
        /// </summary>
        /// <value>The width of the thumbnail large.</value>
        public int ThumbnailLargeWidth { get; set; } = 600;

        /// <summary>
        /// The height of the large thumbnail format, in pixels
        /// </summary>
        /// <value>The height of the thumbnail large.</value>

        public int ThumbnailLargeHeight { get; set; } = 600;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThumbnailGenerator"/> class.
        /// </summary>
        public ThumbnailGenerator() : base()
        {
            string id = Guid.NewGuid().ToString();
            Name = new ElementName(string.Format("ThumbnailGenerator {0} ", id));
            Description = new ElementDescription(string.Format("ThumbnailGenerator {0} ", id));
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string driveLetter = Path.GetPathRoot(assemblyLocation);
            string temporaryImagesFilePath = driveLetter + @"\data\launchpad\images\temp";

            MagickNET.SetTempDirectory(temporaryImagesFilePath);
            string policyMap = @"
                <policymap>
                   <policy domain=""resource"" name=""memory"" value=""3GiB""/> 
                   <policy domain=""resource"" name=""map"" value=""4GiB""/> 
                   <policy domain=""resource"" name=""time"" value=""unlimited""/> 
                </policymap>
            ";
            Configuration = new ImageMagickConfiguration(policyMap, temporaryImagesFilePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThumbnailGenerator"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public ThumbnailGenerator(ImageMagickConfiguration config)
        {
            string id = Guid.NewGuid().ToString();
            Name = new ElementName(string.Format("ThumbnailGenerator {0} ", id));
            Description = new ElementDescription(string.Format("ThumbnailGenerator {0} ", id));
            Configuration = config;
        }

        /// <summary>
        /// Utility method to make a thumbnail using the defined settings including format, width, height
        /// </summary>
        /// <param name="originalImage">A reference to the original image from which the thumbnail is being created, in MagickImage format</param>
        /// <param name="format">The format of the resulting thumbnail (defaults to jpeg)</param>
        /// <param name="width">The width of the resulting thumbnail image</param>
        /// <param name="height">The height of the resulting thumbnail image</param>
        /// <returns>A byte array of the new thumbnail image</returns>
        protected byte[] GetThumbnail(MagickImage originalImage, MagickFormat format, uint width, uint height)
        {
            MagickImage smallThumbnailImage = null;
            MagickReadSettings settings = new MagickReadSettings();
            settings.SetDefine(format, "size", width + "x" + height);
            using (MagickImage newImage = originalImage)
            {
                newImage.Format = format;
                newImage.Strip();
                newImage.Thumbnail(width, height);
                smallThumbnailImage = new MagickImage(newImage);
            }
            return smallThumbnailImage.ToByteArray();
        }

        /// <summary>
        /// Gets the thumbnail small.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailSmall(MagickImage originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailSmallWidth, (uint)ThumbnailSmallHeight);
        }

        /// <summary>
        /// Gets the thumbnail small.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailSmall(byte[] originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailSmallWidth, (uint)ThumbnailSmallHeight);
        }

        /// <summary>
        /// Gets the thumbnail small.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailSmall(Stream originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailSmallWidth, (uint)ThumbnailSmallHeight);
        }

        /// <summary>
        /// Gets the thumbnail small.
        /// </summary>
        /// <param name="originalImageInfo">The original image information.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailSmall(FileInfo originalImageInfo)
        {
            MagickImage magicImage = new MagickImage(originalImageInfo);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailSmallWidth, (uint)ThumbnailSmallHeight);
        }

        /// <summary>
        /// Gets the thumbnail small.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailSmall(byte[] originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, (uint)ThumbnailSmallWidth, (uint)ThumbnailSmallHeight);
            }
            return smallThumbnailImage;
        }

        /// <summary>
        /// Gets the thumbnail small.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailSmall(Stream originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, (uint)ThumbnailSmallWidth, (uint)ThumbnailSmallHeight);
            }
            return smallThumbnailImage;
        }


        /// <summary>
        /// Gets the thumbnail small.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailSmall(MagickImage originalImage, MagickFormat format)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, format, (uint)ThumbnailSmallWidth, (uint)ThumbnailSmallHeight);
        }

        /// <summary>
        /// Gets the thumbnail small.
        /// </summary>
        /// <param name="originalImageInfo">The original image information.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailSmall(FileInfo originalImageInfo, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImageInfo))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, (uint)ThumbnailSmallWidth, (uint)ThumbnailSmallHeight);
            }
            return smallThumbnailImage;
        }

        /// <summary>
        /// Gets the thumbnail medium.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailMedium(MagickImage originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailMediumWidth, (uint)ThumbnailMediumHeight);
        }

        /// <summary>
        /// Gets the thumbnail medium.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailMedium(byte[] originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailMediumWidth, (uint)ThumbnailMediumHeight);
        }

        /// <summary>
        /// Gets the thumbnail medium.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailMedium(Stream originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailMediumWidth, (uint)ThumbnailMediumHeight);
        }

        /// <summary>
        /// Gets the thumbnail medium.
        /// </summary>
        /// <param name="originalImageInfo">The original image information.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailMedium(FileInfo originalImageInfo)
        {
            MagickImage magicImage = new MagickImage(originalImageInfo);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailMediumWidth, (uint)ThumbnailMediumHeight);
        }

        /// <summary>
        /// Gets the thumbnail medium.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailMedium(byte[] originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, (uint)ThumbnailMediumWidth, (uint)ThumbnailMediumHeight);
            }
            return smallThumbnailImage;
        }

        /// <summary>
        /// Gets the thumbnail medium.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailMedium(Stream originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, (uint)ThumbnailMediumWidth, (uint)ThumbnailMediumHeight);
            }
            return smallThumbnailImage;
        }

        /// <summary>
        /// Gets the thumbnail medium.
        /// </summary>
        /// <param name="originalImageInfo">The original image information.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailMedium(FileInfo originalImageInfo, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImageInfo))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, (uint)ThumbnailMediumWidth, (uint)ThumbnailMediumHeight);
            }
            return smallThumbnailImage;
        }

        /// <summary>
        /// Gets the thumbnail medium.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailMedium(MagickImage originalImage, MagickFormat format)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, format, (uint)ThumbnailMediumWidth, (uint)ThumbnailMediumHeight);
        }

        /// <summary>
        /// Gets the thumbnail large.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailLarge(MagickImage originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailLargeWidth, (uint)ThumbnailLargeHeight);
        }

        /// <summary>
        /// Gets the thumbnail large.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailLarge(byte[] originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailLargeWidth, (uint)ThumbnailLargeHeight);
        }

        /// <summary>
        /// Gets the thumbnail large.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailLarge(Stream originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailLargeWidth, (uint)ThumbnailLargeHeight);
        }


        /// <summary>
        /// Gets the thumbnail large.
        /// </summary>
        /// <param name="originalImageInfo">The original image information.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailLarge(FileInfo originalImageInfo)
        {
            MagickImage magicImage = new MagickImage(originalImageInfo);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, (uint)ThumbnailLargeWidth, (uint)ThumbnailLargeHeight);
        }


        /// <summary>
        /// Gets the thumbnail large.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailLarge(byte[] originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, (uint)ThumbnailLargeWidth, (uint)ThumbnailLargeHeight);
            }
            return smallThumbnailImage;
        }

        /// <summary>
        /// Gets the thumbnail large.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailLarge(Stream originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, (uint)ThumbnailLargeWidth, (uint)ThumbnailLargeHeight);
            }
            return smallThumbnailImage;
        }

        /// <summary>
        /// Gets the thumbnail large.
        /// </summary>
        /// <param name="originalImageInfo">The original image information.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailLarge(FileInfo originalImageInfo, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImageInfo))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, (uint)ThumbnailLargeWidth, (uint)ThumbnailLargeHeight);
            }
            return smallThumbnailImage;
        }

        /// <summary>
        /// Gets the thumbnail large.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetThumbnailLarge(MagickImage originalImage, MagickFormat format)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, format, (uint)ThumbnailLargeWidth, (uint)ThumbnailLargeHeight);
        }
    }
}
