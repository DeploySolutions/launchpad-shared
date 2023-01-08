//LaunchPad Shared
// Copyright (c) 2018-2022 Deploy Software Solutions, inc. 

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

using System.Diagnostics.CodeAnalysis;

namespace Deploy.LaunchPad.Images.Domain
{
    using Abp.Domain.Services;
    using Castle.Core.Logging;
    using Deploy.LaunchPad.Core.Util;
    using ImageMagick;
    using System;
    using System.IO;

    /// <summary>
    /// Domain service for handling Image domain entities.
    /// Important: MagickImage which we use has a dependency on ImageMagick, which you can download here: https://imagemagick.org/script/download.php#windows 
    /// (please ensure you are compliant with their licensing).
    /// </summary>
    public class ImageManager : DomainService, IImageManager
    {
        /// <summary>
        /// The general size category of the thumbnail. Default dimensions are set for each size, but can be overriden by a user or developer
        /// </summary>
        public enum ThumbnailSize
        {
            Small = 0,
            Medium = 1,
            Large = 2
        }

        /// <summary>
        /// A reference to the ImageComparer object which compares various images
        /// </summary>
        private readonly ImageComparer _comparer;

        /// <summary>
        /// A reference to the ThumbnailGenerator object which makes thumbnails out of provided images
        /// </summary>
        public readonly ThumbnailGenerator ThumbnailGenerator;

        public ImageManager()
        {
            // Create the default ImageMagick configuration, which also initializes the underlying ImageMagick utility
            string temporaryImagesFilePath = @"f:\data\launchpad\images\temp";
            string policyMap = @"
                <policymap>
                   <policy domain=""resource"" name=""memory"" value=""3GiB""/> 
                   <policy domain=""resource"" name=""map"" value=""4GiB""/> 
                   <policy domain=""resource"" name=""time"" value=""unlimited""/> 
                </policymap>
            ";
            ImageMagickConfiguration config = new ImageMagickConfiguration(policyMap, temporaryImagesFilePath);
            _comparer = new ImageComparer(config);
            ThumbnailGenerator = new ThumbnailGenerator(config);
            Logger = NullLogger.Instance; // logger should be loaded by ABP property injection, but if not don't raise errors
        }

        /// <summary>
        /// Get a MagickImage object from the provided file
        /// </summary>
        /// <param name="imageFile">The image we wish to load</param>
        /// <returns>The image, in MagickImage format</returns>
        public MagickImage GetMagickImageFromFile(FileInfo imageFile)
        {
            MagickImage image;
            try
            {
                image = new MagickImage(imageFile);
            }
            catch (MagickException ex)
            {
                Logger.Error(
                    Deploy_LaunchPad_Images_Resources.Exception_ImageManager_GetMagickImageFromFile_InvalidOperationException
                    + " : "
                    + ex.Message
                );
                throw new InvalidOperationException(Deploy_LaunchPad_Images_Resources.Exception_ImageManager_GetMagickImageFromFile_InvalidOperationException);
            }
            return image;
        }

        /// <summary>
        /// Compare two images, using the provided comparison settings
        /// </summary>
        /// <param name="imageA">The first image in the comparison, in byte array format</param>
        /// <param name="imageB">The second image in the comparison, in byte array format</param>
        /// <param name="settings">ImageMagick comparison settings</param>
        /// <returns>A byte array containing a new image that represents the *difference* between image a and b</returns>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public byte[] CompareImages(byte[] imageA, byte[] imageB, CompareSettings settings)
        {
            Guard.Against<NullReferenceException>(imageA == null, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageA_NullReferenceException);
            Guard.Against<NullReferenceException>(imageB == null, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageB_NullReferenceException);
            Guard.Against<ArgumentOutOfRangeException>(imageA.Length <= 0, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageA_ArgumentOutOfRangeException);
            Guard.Against<ArgumentOutOfRangeException>(imageB.Length <= 0, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageB_ArgumentOutOfRangeException);
            byte[] diffImage;
            try
            {
                diffImage = CompareImages(new MagickImage(imageA), new MagickImage(imageB), settings);
            }
            catch (MagickMissingDelegateErrorException ex)
            {
                Logger.Error(
                    Deploy_LaunchPad_Images_Resources.Exception_ImageManager_CompareImages_MagickMissingDelegateErrorException
                    + " : "
                   + ex.Message
                );
                throw new ArgumentException(Deploy_LaunchPad_Images_Resources.Exception_ImageManager_CompareImages_MagickMissingDelegateErrorException);
            }
            return diffImage;
        }

        /// <summary>
        /// Compare two images, using the provided comparison settings
        /// </summary>
        /// <param name="imageA">The first image in the comparison, in MagickImage native format</param>
        /// <param name="imageB">The second image in the comparison, in MagickImage native format</param>
        /// <param name="settings">ImageMagick comparison settings</param>
        /// <returns>A byte array containing a new image that represents the *difference* between image a and b</returns>
        public byte[] CompareImages(MagickImage imageA, MagickImage imageB, CompareSettings settings)
        {
            Guard.Against<NullReferenceException>(imageA == null, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageA_NullReferenceException);
            Guard.Against<NullReferenceException>(imageB == null, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageB_NullReferenceException);
            byte[] diffImage;
            try
            {
                diffImage = _comparer.Compare(imageA, imageB, settings);
            }
            catch (MagickMissingDelegateErrorException ex)
            {
                Logger.Error(
                   Deploy_LaunchPad_Images_Resources.Exception_ImageManager_CompareImages_MagickMissingDelegateErrorException
                   + " : "
                   + ex.Message
               );
                throw new ArgumentException(Deploy_LaunchPad_Images_Resources.Exception_ImageManager_CompareImages_MagickMissingDelegateErrorException);
            }
            return diffImage;
        }

        /// <summary>
        /// Creates a thumbnail file from the provide image, set to the specified dimensions. The default format is JPEG.
        /// </summary>
        /// <param name="originalImage">The image source from which we will create the thumbnail</param>
        /// <param name="size">The general size category of the resulting thumbnail.
        /// Default dimensions are set for each size, but can be overriden by a user or developer</param>
        /// <returns>A <see cref="byte"/> array containing a new image that represents the thumbnail, in the appropriate size</returns>
        public byte[] GetThumbnailFromImage(byte[] originalImage, ThumbnailSize size)
        {
            Guard.Against<ArgumentOutOfRangeException>(originalImage.Length <= 0, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_OriginalImage_ArgumentOutOfRangeException);
            byte[] thumbImage = null;
            if (size == ThumbnailSize.Small)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailSmall(originalImage, MagickFormat.Jpeg);
            }
            else if (size == ThumbnailSize.Medium)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailMedium(originalImage, MagickFormat.Jpeg);
            }
            else if (size == ThumbnailSize.Large)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailLarge(originalImage, MagickFormat.Jpeg);
            }
            return thumbImage;
        }


        /// <summary>
        /// Creates a thumbnail file from the provide image, set to the specified dimensions
        /// </summary>
        /// <param name="originalImage">The image source from which we will create the thumbnail</param>
        /// <param name="size">The general size category of the resulting thumbnail.
        /// Default dimensions are set for each size, but can be overriden by a user or developer</param>
        /// <param name="format">The file format of the resulting thumbnail.</param>
        /// <returns>A <see cref="byte"/> array containing a new image that represents the thumbnail, in the appropriate size</returns>
        public byte[] GetThumbnailFromImage(byte[] originalImage, ThumbnailSize size, MagickFormat format)
        {
            Guard.Against<ArgumentOutOfRangeException>(originalImage.Length <= 0, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_OriginalImage_ArgumentOutOfRangeException);
            byte[] thumbImage = null;
            if (size == ThumbnailSize.Small)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailSmall(originalImage, format);
            }
            else if (size == ThumbnailSize.Medium)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailMedium(originalImage, format);
            }
            else if (size == ThumbnailSize.Large)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailLarge(originalImage, format);
            }
            return thumbImage;
        }

        /// <summary>
        /// Creates a thumbnail file from the provide image, set to the specified dimensions. The default format is JPEG.
        /// </summary>
        /// <param name="originalImage">The image source from which we will create the thumbnail</param>
        /// <param name="size">The general size category of the resulting thumbnail.
        /// Default dimensions are set for each size, but can be overriden by a user or developer</param>
        /// <returns>A <see cref="byte"/> array containing a new image that represents the thumbnail, in the appropriate size</returns>
        public byte[] GetThumbnailFromImage(MagickImage originalImage, ThumbnailSize size)
        {
            byte[] thumbImage = null;
            Guard.Against<ArgumentNullException>(originalImage == null, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_OriginalImage_ArgumentNullException);
            if (size == ThumbnailSize.Small)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailSmall(originalImage, MagickFormat.Jpeg);
            }
            else if (size == ThumbnailSize.Medium)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailMedium(originalImage, MagickFormat.Jpeg);
            }
            else if (size == ThumbnailSize.Large)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailLarge(originalImage, MagickFormat.Jpeg);
            }
            return thumbImage;
        }


        /// <summary>
        /// Creates a thumbnail file from the provide image, set to the specified dimensions
        /// </summary>
        /// <param name="originalImage">The image source from which we will create the thumbnail</param>
        /// <param name="size">The general size category of the resulting thumbnail.
        /// Default dimensions are set for each size, but can be overriden by a user or developer</param>
        /// <param name="format">The file format of the resulting thumbnail.</param>
        /// <returns>A <see cref="byte"/> array containing a new image that represents the thumbnail, in the appropriate size</returns>
        public byte[] GetThumbnailFromImage(MagickImage originalImage, ThumbnailSize size, MagickFormat format)
        {
            Guard.Against<ArgumentOutOfRangeException>(originalImage == null, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_OriginalImage_ArgumentNullException);
            byte[] thumbImage = null;
            if (size == ThumbnailSize.Small)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailSmall(originalImage, format);
            }
            else if (size == ThumbnailSize.Medium)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailMedium(originalImage, format);
            }
            else if (size == ThumbnailSize.Large)
            {
                thumbImage = ThumbnailGenerator.GetThumbnailLarge(originalImage, format);
            }
            return thumbImage;
        }

        /// <summary>
        /// Override the default or current thumbnail settings with a new width and height, for the corresponding thumbnail size category
        /// </summary>
        /// <param name="size">The thumbnail size category we wish to override</param>
        /// <param name="width">The new width of this category</param>
        /// <param name="height">The new height of this category</param>
        public void SetThumbnailSizeDimensions(ThumbnailSize size, int width, int height)
        {
            Guard.Against<ArgumentOutOfRangeException>(width <= 0, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_Width_ArgumentOutOfRangeException);
            Guard.Against<ArgumentOutOfRangeException>(height <= 0, Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_Height_ArgumentOutOfRangeException);
            if (size == ThumbnailSize.Small)
            {
                ThumbnailGenerator.ThumbnailSmallWidth = width;
                ThumbnailGenerator.ThumbnailSmallHeight = height;
            }
            else if (size == ThumbnailSize.Medium)
            {
                ThumbnailGenerator.ThumbnailMediumWidth = width;
                ThumbnailGenerator.ThumbnailMediumHeight = height;
            }
            else if (size == ThumbnailSize.Large)
            {
                ThumbnailGenerator.ThumbnailLargeWidth = width;
                ThumbnailGenerator.ThumbnailLargeHeight = height;
            }

        }

    }
}
