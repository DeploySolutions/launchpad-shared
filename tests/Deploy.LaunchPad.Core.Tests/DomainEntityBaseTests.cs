// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DomainEntityBaseTests.cs" company="Deploy.LaunchPad.Core.Tests">
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

namespace Deploy.LaunchPad.Domain.Tests
{
    using Xunit;
    
    using Deploy.LaunchPad.Domain;
    using Deploy.LaunchPad.Util;
    using Deploy.LaunchPad.Core.Abp;
    using Deploy.LaunchPad.Core.Abp.Devices;

    /// <summary>
    /// Class DomainEntityBaseTests.
    /// </summary>
    public partial class DomainEntityBaseTests
    {
        #region "Test Classes"



        #endregion


        /// <summary>
        /// Defines the test method Should_Not_Have_Null_TenantId_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Not_Have_Null_TenantId_When_Instantiated()
        {
            Device<System.Guid> a = new Device<System.Guid>();

            Assert.False(a.TenantId.HasValue);
        }

        /// <summary>
        /// Defines the test method Should_Have_Empty_Id_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_Empty_Id_When_Instantiated()
        {
            Device<System.Guid> a = new Device<System.Guid>();
            Assert.Equal(System.Guid.Empty, a.Id);
        }

        /// <summary>
        /// Defines the test method Should_NotHave_Empty_Id_When_Instantiated_With_Id.
        /// </summary>
        [Fact]
        public void Should_NotHave_Empty_Id_When_Instantiated_With_Id()
        {
            var id = SequentialGuid.NewGuid();
            Device<System.Guid> a = new Device<System.Guid>(null, id);
            Assert.NotEqual(System.Guid.Empty, a.Id);
        }

        /// <summary>
        /// Defines the test method Should_Have_Unique_Id_When_Instantiated_With_Id.
        /// </summary>
        [Fact]
        public void Should_Have_Unique_Id_When_Instantiated_With_Id()
        {
            var aId = SequentialGuid.NewGuid();
            Device<System.Guid> a = new Device<System.Guid>(null, aId);
            var bId = SequentialGuid.NewGuid();
            Device<System.Guid> b = new Device<System.Guid>(null, bId);
            Assert.NotEqual(b.Id, a.Id);
        }



    }
}
