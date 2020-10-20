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
    using DeploySoftware.LaunchPad.Core.Util;

    public class DomainEntityBaseTests
    {
        #region "Test Classes"



        #endregion


        [Fact]
        public void Should_Not_Have_Null_TenantId_When_Instantiated()
        {
            Device<System.Guid> a = new Device<System.Guid>();
            //a.TenantId.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_Empty_Id_When_Instantiated()
        {
            Device<System.Guid> a = new Device<System.Guid>();
            a.Id.Should().BeEmpty();
        }

        [Fact]
        public void Should_NotHave_Empty_Id_When_Instantiated_With_Id()
        {
            var id = SequentialGuid.NewGuid();
            Device<System.Guid> a = new Device<System.Guid>(null, id);
            a.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void Should_Have_Unique_Id_When_Instantiated_With_Id()
        {
            var aId = SequentialGuid.NewGuid();
            Device<System.Guid> a = new Device<System.Guid>(null, aId);
            var bId = SequentialGuid.NewGuid();
            Device<System.Guid> b = new Device<System.Guid>(null, bId);
            a.Id.Should().NotBe(b.Id);
        }



    }
}
