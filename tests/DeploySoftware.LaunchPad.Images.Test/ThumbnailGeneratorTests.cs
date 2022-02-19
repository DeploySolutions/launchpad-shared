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

using Abp.Reflection.Extensions;

namespace DeploySoftware.LaunchPad.Images.Tests
{
    using FluentAssertions;
    using System;
    using System.IO;
    using Xunit;
    using DeploySoftware.LaunchPad.Images.Domain;
    using ImageMagick;


    public class ThumbnailGeneratorTests : IClassFixture<ThumbnailGeneratorTestsFixture>
    {
        private readonly ThumbnailGeneratorTestsFixture _fixture;

        public ThumbnailGeneratorTests(ThumbnailGeneratorTestsFixture fixture)
        {
            this._fixture = fixture;
            
            ThumbnailGenerator generator = new ThumbnailGenerator();
            CompareSettings compareSettings = new CompareSettings();
            this._fixture.Initialize(generator, compareSettings);
        }

        [Fact]
        public void New_Thumbnail_Generator_DefaultConstructor_Configuration_ShouldNot_Be_Null()
        {
            ThumbnailGenerator generator = new ThumbnailGenerator();
            generator.Configuration.Should().NotBeNull();
        }
        
        [Fact]
        public void New_Thumbnail_Generator_ConfigurationConstructor_Configuration_ShouldNot_Be_Null()
        {
            // Create the default ImageMagick configuration, which also initializes the underlying ImageMagick utility
            string temporaryImagesFilePath = @"f:\data\launchpad\images\temp";
            MagickNET.SetTempDirectory(temporaryImagesFilePath);
            string policyMap = @"
                <policymap>
                   <policy domain=""resource"" name=""memory"" value=""3GiB""/> 
                   <policy domain=""resource"" name=""map"" value=""4GiB""/> 
                   <policy domain=""resource"" name=""time"" value=""unlimited""/> 
                </policymap>
            ";
            ImageMagickConfiguration config = new ImageMagickConfiguration(policyMap, temporaryImagesFilePath);
            ThumbnailGenerator generator = new ThumbnailGenerator(config);
            generator.Configuration.Should().NotBeNull();
        }

        [Fact]
        public void New_Thumbnail_Generator_Custom_Configuration_TempFilePath_That_DoesNot_Exist_Should_Throw()
        {
            // Create the default ImageMagick configuration, which also initializes the underlying ImageMagick utility
            string temporaryImagesFilePath = @"filepathdoesnotexist";
            string policyMap = @"
                <policymap>
                   <policy domain=""resource"" name=""memory"" value=""3GiB""/> 
                   <policy domain=""resource"" name=""map"" value=""4GiB""/> 
                   <policy domain=""resource"" name=""time"" value=""unlimited""/> 
                </policymap>
            ";
            ThumbnailGenerator generator;
            Action act = () => generator = new ThumbnailGenerator(new ImageMagickConfiguration(policyMap, temporaryImagesFilePath));
            act.Should().Throw<ArgumentException>();
            
        }


        [Fact]
        public void New_Thumbnail_Generator_Custom_Configuration_TempFilePath_Should_Be_Set()
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
            ThumbnailGenerator generator = new ThumbnailGenerator(config);
            generator.Configuration.TemporaryImagesFilePath.Should().Contain("temp");
        }
        
        // small thumbnail tests
        [Fact]
        public void Get_Small_Thumbnail_From_MagickImage_ShouldReturn_Image()
        {
            MagickImage image = new MagickImage(_fixture.Logo);
            byte[] thumbImage = _fixture.SUT.GetThumbnailSmall(image);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Small_Thumbnail_From_ByteArray_ShouldReturn_Image()
        {
            byte[] thumbImage = _fixture.SUT.GetThumbnailSmall(_fixture.Logo);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Small_Thumbnail_From_Stream_ShouldReturn_Image()
        {
            Stream stream = new MemoryStream(_fixture.Logo);
            byte[] thumbImage = _fixture.SUT.GetThumbnailSmall(stream);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Small_Thumbnail_From_FileInfo_ShouldReturn_Image()
        {
            FileInfo info = new FileInfo(_fixture.LogoFilePath);
            byte[] thumbImage = _fixture.SUT.GetThumbnailSmall(info);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Small_Thumbnail_In_Png_Format_From_MagickImage_Should_Return_Image_In_Png_Format()
        {
            Stream stream = new MemoryStream(_fixture.Logo);
            MagickImage result = new MagickImage(_fixture.SUT.GetThumbnailSmall(stream,MagickFormat.Png));
            result.Format.Should().Be(MagickFormat.Png);
        }
        
        [Fact]
        public void Get_Small_Thumbnail_In_Png_Format_From_FileInfo_Should_Return_Image_In_Png_Format()
        {
            FileInfo info = new FileInfo(_fixture.LogoFilePath);
            MagickImage result = new MagickImage(_fixture.SUT.GetThumbnailSmall(info,MagickFormat.Png));
            result.Format.Should().Be(MagickFormat.Png);
        }

        // medium thumbnail tests
        [Fact]
        public void Get_Medium_Thumbnail_From_MagickImage_ShouldReturn_Image()
        {
            MagickImage image = new MagickImage(_fixture.Logo);
            byte[] thumbImage = _fixture.SUT.GetThumbnailMedium(image);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Medium_Thumbnail_From_ByteArray_ShouldReturn_Image()
        {
            byte[] thumbImage = _fixture.SUT.GetThumbnailMedium(_fixture.Logo);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Medium_Thumbnail_From_Stream_ShouldReturn_Image()
        {
            Stream stream = new MemoryStream(_fixture.Logo);
            byte[] thumbImage = _fixture.SUT.GetThumbnailMedium(stream);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Medium_Thumbnail_From_FileInfo_ShouldReturn_Image()
        {
            FileInfo info = new FileInfo(_fixture.LogoFilePath);
            byte[] thumbImage = _fixture.SUT.GetThumbnailMedium(info);
            thumbImage.Length.Should().BeGreaterThan(0);
        }
       
        [Fact]
        public void Get_Medium_Thumbnail_With_Png_Format_From_MagickImage_ShouldReturn_Png_Image()
        {
            MagickImage image = new MagickImage(_fixture.Logo);
            byte[] thumbImage = _fixture.SUT.GetThumbnailMedium(image, MagickFormat.Png);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Medium_Thumbnail_In_Png_Format_From_MagickImage_Should_Return_Image_In_Png_Format()
        {
            Stream stream = new MemoryStream(_fixture.Logo);
            MagickImage result = new MagickImage(_fixture.SUT.GetThumbnailMedium(stream,MagickFormat.Png));
            result.Format.Should().Be(MagickFormat.Png);
        }
        
        [Fact]
        public void Get_Medium_Thumbnail_In_Png_Format_From_FileInfo_Should_Return_Image_In_Png_Format()
        {
            FileInfo info = new FileInfo(_fixture.LogoFilePath);
            MagickImage result = new MagickImage(_fixture.SUT.GetThumbnailMedium(info,MagickFormat.Png));
            result.Format.Should().Be(MagickFormat.Png);
        }

        // large thumbnail tests
        [Fact]
        public void Get_Large_Thumbnail_From_MagickImage_ShouldReturn_Image()
        {
            MagickImage image = new MagickImage(_fixture.Logo);
            byte[] thumbImage = _fixture.SUT.GetThumbnailLarge(image);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Large_Thumbnail_From_ByteArray_ShouldReturn_Image()
        {
            byte[] thumbImage = _fixture.SUT.GetThumbnailLarge(_fixture.Logo);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Large_Thumbnail_From_Stream_ShouldReturn_Image()
        {
            Stream stream = new MemoryStream(_fixture.Logo);
            byte[] thumbImage = _fixture.SUT.GetThumbnailLarge(stream);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Large_Thumbnail_From_FileInfo_ShouldReturn_Image()
        {
            FileInfo info = new FileInfo(_fixture.LogoFilePath);
            byte[] thumbImage = _fixture.SUT.GetThumbnailLarge(info);
            thumbImage.Length.Should().BeGreaterThan(0);
        }
       
        [Fact]
        public void Get_Large_Thumbnail_With_Png_Format_From_MagickImage_ShouldReturn_Png_Image()
        {
            MagickImage image = new MagickImage(_fixture.Logo);
            byte[] thumbImage = _fixture.SUT.GetThumbnailLarge(image, MagickFormat.Png);
            thumbImage.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Get_Large_Thumbnail_In_Png_Format_From_MagickImage_Should_Return_Image_In_Png_Format()
        {
            Stream stream = new MemoryStream(_fixture.Logo);
            MagickImage result = new MagickImage(_fixture.SUT.GetThumbnailLarge(stream,MagickFormat.Png));
            result.Format.Should().Be(MagickFormat.Png);
        }
        
        [Fact]
        public void Get_Large_Thumbnail_In_Png_Format_From_FileInfo_Should_Return_Image_In_Png_Format()
        {
            FileInfo info = new FileInfo(_fixture.LogoFilePath);
            MagickImage result = new MagickImage(_fixture.SUT.GetThumbnailLarge(info,MagickFormat.Png));
            result.Format.Should().Be(MagickFormat.Png);
        }

    }
}
