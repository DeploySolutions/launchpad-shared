// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Images
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="IImageManager.cs" company="Deploy Software Solutions, inc.">
//     2016-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Deploy.LaunchPad.Images.Domain
{
    using Deploy.LaunchPad.Code.Services;
    using ImageMagick;
    using System;
    using System.IO;

    /// <summary>
    /// Domain service interface for handling Image domain entities
    /// </summary>
    public partial interface IImageManager : ILaunchPadSystemIntegrationService
    {
        /// <summary>
        /// Get a MagickImage object from the provided file
        /// </summary>
        /// <param name="imageFile">The image we wish to load</param>
        /// <returns>The image, in MagickImage format</returns>
        MagickImage GetMagickImageFromFile(FileInfo imageFile);

        /// <summary>
        /// Compare two images, using the provided comparison settings
        /// </summary>
        /// <param name="imageA">The first image in the comparison, in byte array format</param>
        /// <param name="imageB">The second image in the comparison, in byte array format</param>
        /// <param name="settings">ImageMagick comparison settings</param>
        /// <returns>A byte array containing a new image that represents the *difference* between image a and b</returns>
        byte[] CompareImages(byte[] imageA, byte[] imageB, CompareSettings settings);

        /// <summary>
        /// Compare two images, using the provided comparison settings
        /// </summary>
        /// <param name="imageA">The first image in the comparison, in MagickImage native format</param>
        /// <param name="imageB">The second image in the comparison, in MagickImage native format</param>
        /// <param name="settings">ImageMagick comparison settings</param>
        /// <returns>A byte array containing a new image that represents the *difference* between image a and b</returns>
        byte[] CompareImages(MagickImage imageA, MagickImage imageB, CompareSettings settings);

        /// <summary>
        /// Creates a thumbnail file from the provide image, set to the specified dimensions
        /// </summary>
        /// <param name="originalImage">The image source from which we will create the thumbnail</param>
        /// <param name="size">The general size category of the resultin thumbnail. Default dimensions are set for each size, but can be overriden by a user or developer</param>
        /// <returns>A byte array containing a new image that represents the thumbnail, in the appropriate size</returns>
        byte[] GetThumbnailFromImage(byte[] originalImage, ImageManager.ThumbnailSize size);

        /// <summary>
        /// Override the default or current thumbnail settings with a new width and height, for the corresponding thumbnail size category
        /// </summary>
        /// <param name="size">The thumbnail size category we wish to override</param>
        /// <param name="width">The new width of this category</param>
        /// <param name="height">The new height of this category</param>
        void SetThumbnailSizeDimensions(ImageManager.ThumbnailSize size, int width, int height);

        bool ConvertToWebp(string inputImageFilePath, string outputWebpFilePath, uint quality = 75);
    }
}