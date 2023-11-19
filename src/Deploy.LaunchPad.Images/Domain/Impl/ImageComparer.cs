// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Images
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="ImageComparer.cs" company="Deploy Software Solutions, inc.">
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


namespace Deploy.LaunchPad.Images.Domain
{
    using ImageMagick;

    /// <summary>
    /// This class compares two provided images, using Magick.NET library (ImageMagick wrapper).
    /// </summary>
    public partial class ImageComparer
    {

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly ImageMagickConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageComparer"/> class.
        /// </summary>
        protected ImageComparer()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageComparer"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public ImageComparer(ImageMagickConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Utility method to compare two images
        /// </summary>
        /// <param name="imageA">A reference to the first image which will be compared against, in MagickImage format</param>
        /// <param name="imageB">A reference to the second image which will be compared against, in MagickImage format</param>
        /// <param name="compareSettings">The compare settings.</param>
        /// <returns>A byte array of a new image displaying the differences, if any</returns>
        public byte[] Compare(MagickImage imageA, MagickImage imageB, CompareSettings compareSettings)
        {
            using (MagickImage magicDiffImage = new MagickImage())
            {
                magicDiffImage.Format = MagickFormat.Tif;
                imageA.ColorFuzz = new Percentage(50); // set a fairly low percentage to identify differences, without picking up every tiny artefact

                imageA.Compare(imageB, compareSettings, magicDiffImage);
                return magicDiffImage.ToByteArray();
            }
        }

    }
}
