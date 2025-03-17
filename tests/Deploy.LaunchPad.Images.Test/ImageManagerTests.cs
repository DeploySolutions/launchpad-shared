// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Images.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="ImageManagerTests.cs" company="Deploy.LaunchPad.Images.Tests">
//     Copyright (c) Deploy Software Solutions, Inc.. All rights reserved.
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

namespace Deploy.LaunchPad.Images.Tests
{
    using FluentAssertions;
    using System;
    using System.IO;
    using Xunit;
    using Deploy.LaunchPad.Images.Domain;
    using ImageMagick;


    /// <summary>
    /// Class ImageManagerTests.
    /// Implements the <see cref="Xunit.IClassFixture{Deploy.LaunchPad.Images.Tests.ImageManagerTestsFixture}" />
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture{Deploy.LaunchPad.Images.Tests.ImageManagerTestsFixture}" />
    public partial class ImageManagerTests : IClassFixture<ImageManagerTestsFixture>
    {
        /// <summary>
        /// The fixture
        /// </summary>
        private readonly ImageManagerTestsFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageManagerTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public ImageManagerTests(ImageManagerTestsFixture fixture)
        {
            this._fixture = fixture;
            ImageManager imageMan = new ImageManager();
            ErrorMetric metric = ErrorMetric.Undefined;
            CompareSettings compareSettings = new CompareSettings(metric);
            this._fixture.Initialize(imageMan, compareSettings);
        }

        /// <summary>
        /// Defines the test method CompareImages_ImageA_Should_NotBe_Null.
        /// </summary>
        [Fact]
        public void CompareImages_ImageA_Should_NotBe_Null()
        {
            byte[] imageA = null;
            byte[] imageB = _fixture.NotEmptyBytes;
            
            Action act = () => _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            act.Should().Throw<NullReferenceException>()
                 .WithMessage(Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageA_NullReferenceException);
        }

        /// <summary>
        /// Defines the test method CompareImages_Invalid_Image_Formats_Should_Throw_ArgumentException.
        /// </summary>
        [Fact]
        public void CompareImages_Invalid_Image_Formats_Should_Throw_ArgumentException()
        {
            byte[] imageA = _fixture.NotEmptyBytes;
            byte[] imageB = _fixture.NotEmptyBytes;
            
            Action act = () => _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            act.Should().Throw<ArgumentException>()
                .WithMessage(Deploy_LaunchPad_Images_Resources.Exception_ImageManager_CompareImages_MagickMissingDelegateErrorException);
        }

        /// <summary>
        /// Defines the test method CompareImages_ImageB_Should_NotBe_Null.
        /// </summary>
        [Fact]
        public void CompareImages_ImageB_Should_NotBe_Null()
        {
            byte[] imageA = _fixture.NotEmptyBytes;
            byte[] imageB = null;
            Action act = () =>  _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            act.Should().Throw<NullReferenceException>()
                 .WithMessage(Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageB_NullReferenceException);
        }

        /// <summary>
        /// Defines the test method CompareImages_DifferenceImage_Should_notBe_Null.
        /// </summary>
        [Fact]
        public void CompareImages_DifferenceImage_Should_notBe_Null()
        {
            MagickImage imageA = new MagickImage(_fixture.LogoWhite);
            MagickImage imageB = new MagickImage(_fixture.LogoDark);

            byte[] diffImage = _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            diffImage.Should().NotBeNull();
        }

        /// <summary>
        /// Defines the test method GetMagickImageFromFile_Invalid_FilePath_Should_Throw_InvalidOperationException.
        /// </summary>
        [Fact]
        public void GetMagickImageFromFile_Invalid_FilePath_Should_Throw_InvalidOperationException()
        {
            FileInfo info = new FileInfo(@"not_a_valid_path");

            Action act = () => _fixture.SUT.GetMagickImageFromFile(info);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(Deploy_LaunchPad_Images_Resources.Exception_ImageManager_GetMagickImageFromFile_InvalidOperationException);
        }

        /// <summary>
        /// Defines the test method Default_Small_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_100.
        /// </summary>
        [Fact]
        public void Default_Small_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_100()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Small));
            image.Width.Should().BeLessThanOrEqualTo(100);
        }

        /// <summary>
        /// Defines the test method Default_Small_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_100.
        /// </summary>
        [Fact]
        public void Default_Small_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_100()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Small));
            image.Height.Should().BeLessThanOrEqualTo(100);
        }

        /// <summary>
        /// Defines the test method Default_Medium_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_300.
        /// </summary>
        [Fact]
        public void Default_Medium_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_300()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Medium));
            image.Width.Should().BeLessThanOrEqualTo(300);
        }

        /// <summary>
        /// Defines the test method Default_Medium_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_300.
        /// </summary>
        [Fact]
        public void Default_Medium_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_300()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Medium));
            image.Height.Should().BeLessThanOrEqualTo(300);
        }

        /// <summary>
        /// Defines the test method Default_Large_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_600.
        /// </summary>
        [Fact]
        public void Default_Large_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_600()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Large));
            image.Width.Should().BeLessThanOrEqualTo(600);
        }

        /// <summary>
        /// Defines the test method Default_Large_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_600.
        /// </summary>
        [Fact]
        public void Default_Large_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_600()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Large));
            image.Height.Should().BeLessThanOrEqualTo(600);
        }

        /// <summary>
        /// Defines the test method Custom_Small_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_99.
        /// </summary>
        [Fact]
        public void Custom_Small_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_99()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Small,99,99);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Small));
            image.Width.Should().BeLessThanOrEqualTo(99);
        }

        /// <summary>
        /// Defines the test method Custom_Small_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_99.
        /// </summary>
        [Fact]
        public void Custom_Small_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_99()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Small,99,99);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Small));
            image.Height.Should().BeLessThanOrEqualTo(99);
        }

        /// <summary>
        /// Defines the test method Custom_Medium_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_299.
        /// </summary>
        [Fact]
        public void Custom_Medium_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_299()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Medium,299,299);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Medium));
            image.Width.Should().BeLessThanOrEqualTo(299);
        }

        /// <summary>
        /// Defines the test method Custom_Medium_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_299.
        /// </summary>
        [Fact]
        public void Custom_Medium_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_299()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Medium,299,299);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Medium));
            image.Height.Should().BeLessThanOrEqualTo(299);
        }

        /// <summary>
        /// Defines the test method Custom_Large_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_599.
        /// </summary>
        [Fact]
        public void Custom_Large_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_599()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Large,599,599);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Large));
            image.Width.Should().BeLessThanOrEqualTo(599);
        }

        /// <summary>
        /// Defines the test method Custom_Large_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_599.
        /// </summary>
        [Fact]
        public void Custom_Large_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_599()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Large,599,599);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Large));
            image.Height.Should().BeLessThanOrEqualTo(599);
        }


        /// <summary>
        /// Defines the test method Get_Small_Thumbnail_From_MagickImage_ShouldReturn_JpegFormat.
        /// </summary>
        [Fact]
        public void Get_Small_Thumbnail_From_MagickImage_ShouldReturn_JpegFormat()
        {
            MagickImage result = new MagickImage(_fixture.SUT.GetThumbnailFromImage(new MagickImage(_fixture.LogoWhite), ImageManager.ThumbnailSize.Small,MagickFormat.Png));
            result.Format.Should().Be(MagickFormat.Png);

        }

        /// <summary>
        /// Defines the test method Get_Small_Thumbnail_From_MagickImage_ShouldReturn_Image.
        /// </summary>
        [Fact]
        public void Get_Small_Thumbnail_From_MagickImage_ShouldReturn_Image()
        {
            byte[] logo = _fixture.LogoDark;
            MagickImage image = new MagickImage(logo);
            byte[] thumbImage = _fixture.SUT.GetThumbnailFromImage(image, ImageManager.ThumbnailSize.Small);
            thumbImage.Length.Should().BeGreaterThan(0);
        }
    }
}
