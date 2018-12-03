﻿//LaunchPad Shared
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


namespace DeploySoftware.LaunchPad.Shared.Tests
{
    
    using FluentAssertions;
    using Xunit;

    public class TifWorldFileTests : IClassFixture<TfwWorldFileTestsFixture>
    {
        
        private readonly TfwWorldFileTestsFixture _fixture;

        public TifWorldFileTests(TfwWorldFileTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void FileExtension_Should_Be_DotTFW()
        {
            
            _fixture.TfwFile.FileExtension.Should().Be(".tfw");
        }
       
        [Fact]
        public void A_Should_Equal_First_Line_In_File()
        {
            _fixture.TfwFile.A.Should().Be(12.413247108000000m);
        }
        
        [Fact]
        public void D_Should_Equal_Second_Line_In_File()
        {
            _fixture.TfwFile.D.Should().Be(0.000000000000000m);
        }

        [Fact]
        public void B_Should_Equal_Third_Line_In_File()
        {
            _fixture.TfwFile.B.Should().Be(0.000000000000000m);
        }

        [Fact]
        public void E_Should_Equal_Fourth_Line_In_File()
        {
            _fixture.TfwFile.E.Should().Be(-12.382885933000001m);
        }
        
        [Fact]
        public void C_Should_Equal_Fifth_Line_In_File()
        {
            _fixture.TfwFile.C.Should().Be(511283.0285078580m);
        }
        
        [Fact]
        public void F_Should_Equal_Sixth_Line_In_File()
        {
            _fixture.TfwFile.F.Should().Be(7755841.0522885220m);
        }
        
    }
}
