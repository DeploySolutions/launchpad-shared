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

namespace DeploySoftware.LaunchPad.Shared.Tests
{
    using Xunit;
    using FluentAssertions;
    using DeploySoftware.LaunchPad.Shared.Domain;
    using DeploySoftware.LaunchPad.Shared.Util;

    public class DomainEntityKeyTests
    {
        #region "Test Classes"

       

        #endregion
        

        [Fact]
        public void Should_Have_NotEmpty_Id_When_Instantiated()
        {
            DomainEntityKey<System.Guid> key = new DomainEntityKey<System.Guid>(
                SequentialGuid.NewGuid(), ApplicationSettings<System.Guid>.DEFAULT_CULTURE);
            key.Id.Should().NotBeEmpty();

        }

        [Fact]
        public void Should_Have_Unique_Id_When_Instantiated()
        {
            DomainEntityKey<System.Guid> key1 = new DomainEntityKey<System.Guid>(SequentialGuid.NewGuid()
                , ApplicationSettings<System.Guid>.DEFAULT_CULTURE);
            DomainEntityKey<System.Guid> key2 = new DomainEntityKey<System.Guid>(SequentialGuid.NewGuid()
                , ApplicationSettings<System.Guid>.DEFAULT_CULTURE);
            key1.Should().NotBe(key2);
        }

        [Fact]
        public void Should_Have_NotNull_Culture_When_Instantiated()
        {
            DomainEntityKey<System.Guid> key = new DomainEntityKey<System.Guid>(SequentialGuid.NewGuid()
                , ApplicationSettings<System.Guid>.DEFAULT_CULTURE);
            key.Culture.Should().NotBeNull();
        }
    }
}
