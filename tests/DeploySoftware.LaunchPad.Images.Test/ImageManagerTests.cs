//LaunchPad Shared
// Copyright (c) 2018 Deploy Software Solutions, inc. 

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


namespace DeploySoftware.LaunchPad.Images.Tests
{
    using FluentAssertions;
    using ImageMagick;
    using System;
    using System.IO;
    using Xunit;

    public class ImageManagerTests : IClassFixture<ImageManagerTestsFixture>
    {
        private readonly ImageManagerTestsFixture _fixture;

        public ImageManagerTests(ImageManagerTestsFixture fixture)
        {
            this._fixture = fixture;
            ImageManager imageMan = new ImageManager();
            this._fixture.Initialize(imageMan);
        }
        
        [Fact]
        public void CompareImages_ImageA_Should_NotBe_Null()
        {
            byte[] imageA = null;
            byte[] imageB = _fixture.NotEmptyBytes;
            
            Action act = () => _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            act.Should().Throw<NullReferenceException>()
                 .WithMessage(DeploySoftware_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageA_NullReferenceException);
        }

        [Fact]
        public void CompareImages_Invalid_Image_Formats_Should_Throw_ArgumentException()
        {
            byte[] imageA = _fixture.NotEmptyBytes;
            byte[] imageB = _fixture.NotEmptyBytes;
            
            Action act = () => _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            act.Should().Throw<ArgumentException>()
                .WithMessage(DeploySoftware_LaunchPad_Images_Resources.Exception_ImageManager_CompareImages_MagickMissingDelegateErrorException);
        }

        [Fact]
        public void CompareImages_ImageB_Should_NotBe_Null()
        {
            byte[] imageA = _fixture.NotEmptyBytes;
            byte[] imageB = null;
            Action act = () =>  _fixture.SUT.CompareImages(imageA, imageB, _fixture.Settings);
            act.Should().Throw<NullReferenceException>()
                 .WithMessage(DeploySoftware_LaunchPad_Images_Resources.Guard_ImageManager_Thumbnail_ImageB_NullReferenceException);
        }


        [Fact]
        public void GetMagickImageFromFile_Invalid_FilePath_Should_Throw_InvalidOperationException()
        {
            FileInfo info = new FileInfo(@"not_a_valid_path");

            Action act = () => _fixture.SUT.GetMagickImageFromFile(info);
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(DeploySoftware_LaunchPad_Images_Resources.Exception_ImageManager_GetMagickImageFromFile_InvalidOperationException);
        }

    }
}
