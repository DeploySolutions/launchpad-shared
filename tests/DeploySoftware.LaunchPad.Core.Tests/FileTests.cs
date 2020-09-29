//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Core.Tests
{
    using Xunit;
    using FluentAssertions;
    using DeploySoftware.LaunchPad.Core.Domain;

    public class FileTests
    {
        #region "Test Classes"



        #endregion



        [Fact]
        public void Should_Have_NotNull_Key_Name_When_Instantiated()
        {
            FileKey key = new FileKey();
            key.Name.Should().NotBeNull();

        }

        [Fact]
        public void Should_Have_Unique_Key_Name_When_Instantiated()
        {
            FileKey key1 = new FileKey();
            FileKey key2 = new FileKey();
            key1.Should().NotBeSameAs(key2);
        }

        [Fact]
        public void ShouldNot_Have_Equal_Name_And_Key_Name_When_Instantiated_Without_Name()
        {
            TifFile<int> file = new TifFile<int>();
            file.Name.Should().NotBeSameAs(file.Key.Name);
        }

        [Fact]
        public void Should_Have_Equal_Name_And_Key_Name_When_Instantiated_With_Name()
        {
            TifFile<int> file = new TifFile<int>("SameName");
            file.Name.Should().BeSameAs(file.Key.Name);
        }
    }
}
