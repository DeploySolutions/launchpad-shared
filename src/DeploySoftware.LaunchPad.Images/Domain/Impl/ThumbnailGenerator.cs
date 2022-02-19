//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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


namespace DeploySoftware.LaunchPad.Images.Domain
{
    using DeploySoftware.LaunchPad.Core.Domain;
    using ImageMagick;
    using System.IO;

    /// <summary>
    /// This class creates thumbnails from provided images, using Magick.NET library (ImageMagick wrapper)
    /// </summary>
    public class ThumbnailGenerator : ILaunchPadDomainService
    {
        public readonly ImageMagickConfiguration Configuration;

        /// <summary>
        /// The width of the small thumbnail format, in pixels
        /// </summary>
        public int ThumbnailSmallWidth { get; set; } = 100;

        /// <summary>
        /// The height of the small thumbnail format, in pixels
        /// </summary>
        public int ThumbnailSmallHeight { get; set; } = 100;

        /// <summary>
        /// The width of the medium thumbnail format, in pixels
        /// </summary>
        public int ThumbnailMediumWidth { get; set; } = 300;

        /// <summary>
        /// The height of the medium thumbnail format, in pixels
        /// </summary>
        public int ThumbnailMediumHeight { get; set; } = 300;

        /// <summary>
        /// The width of the large thumbnail format, in pixels
        /// </summary>
        public int ThumbnailLargeWidth { get; set; } = 600;

        /// <summary>
        /// The height of the large thumbnail format, in pixels
        /// </summary>

        public int ThumbnailLargeHeight { get; set; } = 600;

        public ThumbnailGenerator()
        {
            string temporaryImagesFilePath = @"f:\data\launchpad\images\temp";
            MagickNET.SetTempDirectory(temporaryImagesFilePath);
            MagickAnyCPU.CacheDirectory = temporaryImagesFilePath;
            string policyMap = @"
                <policymap>
                   <policy domain=""resource"" name=""memory"" value=""3GiB""/> 
                   <policy domain=""resource"" name=""map"" value=""4GiB""/> 
                   <policy domain=""resource"" name=""time"" value=""unlimited""/> 
                </policymap>
            ";
            Configuration = new ImageMagickConfiguration(policyMap, temporaryImagesFilePath);
            
        }

        public ThumbnailGenerator(ImageMagickConfiguration config)
        {
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
        protected byte[] GetThumbnail(MagickImage originalImage, MagickFormat format, int width, int height)
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

        public byte[] GetThumbnailSmall(MagickImage originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailSmallWidth, ThumbnailSmallHeight);
        }

        public byte[] GetThumbnailSmall(byte[] originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailSmallWidth, ThumbnailSmallHeight);
        }
        
        public byte[] GetThumbnailSmall(Stream originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailSmallWidth, ThumbnailSmallHeight);
        }

        public byte[] GetThumbnailSmall(FileInfo originalImageInfo)
        {
            MagickImage magicImage = new MagickImage(originalImageInfo);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailSmallWidth, ThumbnailSmallHeight);
        }

        public byte[] GetThumbnailSmall(byte[] originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, ThumbnailSmallWidth, ThumbnailSmallHeight);
            }
            return smallThumbnailImage;
        }

        public byte[] GetThumbnailSmall(Stream originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, ThumbnailSmallWidth, ThumbnailSmallHeight);
            }
            return smallThumbnailImage;
        }


        public byte[] GetThumbnailSmall(MagickImage originalImage, MagickFormat format)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, format, ThumbnailSmallWidth, ThumbnailSmallHeight);
        }

        public byte[] GetThumbnailSmall(FileInfo originalImageInfo, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImageInfo))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, ThumbnailSmallWidth, ThumbnailSmallHeight);
            }
            return smallThumbnailImage;
        }
        
        public byte[] GetThumbnailMedium(MagickImage originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailMediumWidth, ThumbnailMediumHeight);
        }

        public byte[] GetThumbnailMedium(byte[] originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailMediumWidth, ThumbnailMediumHeight);
        }

        public byte[] GetThumbnailMedium(Stream originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailMediumWidth, ThumbnailMediumHeight);
        }

        public byte[] GetThumbnailMedium(FileInfo originalImageInfo)
        {
            MagickImage magicImage = new MagickImage(originalImageInfo);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailMediumWidth, ThumbnailMediumHeight);
        }

        public byte[] GetThumbnailMedium(byte[] originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, ThumbnailMediumWidth, ThumbnailMediumHeight);
            }
            return smallThumbnailImage;
        }

        public byte[] GetThumbnailMedium(Stream originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, ThumbnailMediumWidth, ThumbnailMediumHeight);
            }
            return smallThumbnailImage;
        }

        public byte[] GetThumbnailMedium(FileInfo originalImageInfo, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImageInfo))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, ThumbnailMediumWidth, ThumbnailMediumHeight);
            }
            return smallThumbnailImage;
        }
        
        public byte[] GetThumbnailMedium(MagickImage originalImage, MagickFormat format)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, format, ThumbnailMediumWidth, ThumbnailMediumHeight);
        }
        
        public byte[] GetThumbnailLarge(MagickImage originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailLargeWidth, ThumbnailLargeHeight);
        }

        public byte[] GetThumbnailLarge(byte[] originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailLargeWidth, ThumbnailLargeHeight);
        }

        public byte[] GetThumbnailLarge(Stream originalImage)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailLargeWidth, ThumbnailLargeHeight);
        }


        public byte[] GetThumbnailLarge(FileInfo originalImageInfo)
        {
            MagickImage magicImage = new MagickImage(originalImageInfo);
            return GetThumbnail(magicImage, MagickFormat.Jpeg, ThumbnailLargeWidth, ThumbnailLargeHeight);
        }


        public byte[] GetThumbnailLarge(byte[] originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, ThumbnailLargeWidth, ThumbnailLargeHeight);
            }
            return smallThumbnailImage;
        }

        public byte[] GetThumbnailLarge(Stream originalImage, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImage))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, ThumbnailLargeWidth, ThumbnailLargeHeight);
            }
            return smallThumbnailImage;
        }

        public byte[] GetThumbnailLarge(FileInfo originalImageInfo, MagickFormat format)
        {
            byte[] smallThumbnailImage = null;
            using (MagickImage magicImage = new MagickImage(originalImageInfo))
            {
                smallThumbnailImage = GetThumbnail(magicImage, format, ThumbnailLargeWidth, ThumbnailLargeHeight);
            }
            return smallThumbnailImage;
        }

        public byte[] GetThumbnailLarge(MagickImage originalImage, MagickFormat format)
        {
            MagickImage magicImage = new MagickImage(originalImage);
            return GetThumbnail(magicImage, format, ThumbnailLargeWidth, ThumbnailLargeHeight);
        }
    }
}
