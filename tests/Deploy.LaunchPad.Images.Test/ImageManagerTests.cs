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

namespace Deploy.LaunchPad.Images.Tests
{
    using FluentAssertions;
    using System;
    using System.IO;
    using Xunit;
    using Deploy.LaunchPad.Images.Domain;
    using ImageMagick;


    public class ImageManagerTests : IClassFixture<ImageManagerTestsFixture>
    {
        private readonly ImageManagerTestsFixture _fixture;

        public ImageManagerTests(ImageManagerTestsFixture fixture)
        {
            this._fixture = fixture;
            ImageManager imageMan = new ImageManager();
            CompareSettings compareSettings = new CompareSettings();
            this._fixture.Initialize(imageMan, compareSettings);
        }
        
        [Fact]
        public void CompareImages_ImageA_Should_NotBe_Null()
        {
            byte[] imageA = null;
            byte[] imageB = _fixture.NotEmptyBytes;
            
            Action act = () => _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            act.Should().Throw<NullReferenceException>()
                 .WithMessage(Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageA_NullReferenceException);
        }

        [Fact]
        public void CompareImages_Invalid_Image_Formats_Should_Throw_ArgumentException()
        {
            byte[] imageA = _fixture.NotEmptyBytes;
            byte[] imageB = _fixture.NotEmptyBytes;
            
            Action act = () => _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            act.Should().Throw<ArgumentException>()
                .WithMessage(Deploy_LaunchPad_Images_Resources.Exception_ImageManager_CompareImages_MagickMissingDelegateErrorException);
        }

        [Fact]
        public void CompareImages_ImageB_Should_NotBe_Null()
        {
            byte[] imageA = _fixture.NotEmptyBytes;
            byte[] imageB = null;
            Action act = () =>  _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            act.Should().Throw<NullReferenceException>()
                 .WithMessage(Deploy_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageB_NullReferenceException);
        }

        [Fact]
        public void CompareImages_DifferenceImage_Should_notBe_Null()
        {
            MagickImage imageA = new MagickImage(_fixture.LogoWhite);
            MagickImage imageB = new MagickImage(_fixture.LogoDark);

            byte[] diffImage = _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            diffImage.Should().NotBeNull();
        }

        [Fact]
        public void GetMagickImageFromFile_Invalid_FilePath_Should_Throw_InvalidOperationException()
        {
            FileInfo info = new FileInfo(@"not_a_valid_path");

            Action act = () => _fixture.SUT.GetMagickImageFromFile(info);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(Deploy_LaunchPad_Images_Resources.Exception_ImageManager_GetMagickImageFromFile_InvalidOperationException);
        }

        [Fact]
        public void Default_Small_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_100()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Small));
            image.Width.Should().BeLessOrEqualTo(100);
        }

        [Fact]
        public void Default_Small_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_100()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Small));
            image.Height.Should().BeLessOrEqualTo(100);
        }

        [Fact]
        public void Default_Medium_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_300()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Medium));
            image.Width.Should().BeLessOrEqualTo(300);
        }

        [Fact]
        public void Default_Medium_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_300()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Medium));
            image.Height.Should().BeLessOrEqualTo(300);
        }
        
        [Fact]
        public void Default_Large_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_600()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Large));
            image.Width.Should().BeLessOrEqualTo(600);
        }

        [Fact]
        public void Default_Large_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_600()
        {
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Large));
            image.Height.Should().BeLessOrEqualTo(600);
        }
        
        [Fact]
        public void Custom_Small_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_99()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Small,99,99);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Small));
            image.Width.Should().BeLessOrEqualTo(99);
        }
        
        [Fact]
        public void Custom_Small_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_99()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Small,99,99);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Small));
            image.Height.Should().BeLessOrEqualTo(99);
        }

        [Fact]
        public void Custom_Medium_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_299()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Medium,299,299);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Medium));
            image.Width.Should().BeLessOrEqualTo(299);
        }
        
        [Fact]
        public void Custom_Medium_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_299()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Medium,299,299);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Medium));
            image.Height.Should().BeLessOrEqualTo(299);
        }

        [Fact]
        public void Custom_Large_Thumbnail_Width_ShouldBe_LessThanOrEqualTo_599()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Large,599,599);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Large));
            image.Width.Should().BeLessOrEqualTo(599);
        }
        
        [Fact]
        public void Custom_Large_Thumbnail_Height_ShouldBe_LessThanOrEqualTo_599()
        {
            _fixture.SUT.SetThumbnailSizeDimensions(ImageManager.ThumbnailSize.Large,599,599);
            MagickImage image = new MagickImage(_fixture.SUT.GetThumbnailFromImage(_fixture.LogoWhite, ImageManager.ThumbnailSize.Large));
            image.Height.Should().BeLessOrEqualTo(599);
        }

        
        [Fact]
        public void Get_Small_Thumbnail_From_MagickImage_ShouldReturn_JpegFormat()
        {
            MagickImage result = new MagickImage(_fixture.SUT.GetThumbnailFromImage(new MagickImage(_fixture.LogoWhite), ImageManager.ThumbnailSize.Small,MagickFormat.Png));
            result.Format.Should().Be(MagickFormat.Png);

        }

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
