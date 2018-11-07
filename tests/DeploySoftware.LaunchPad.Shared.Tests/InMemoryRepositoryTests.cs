//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 

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
    using System.Collections.Generic;
    using DeploySoftware.LaunchPad.Shared.Repositories;
    using DeploySoftware.LaunchPad.Shared.Specifications;
    using DeploySoftware.LaunchPad.Shared.Util;

    public class InMemoryRepositoryTests
    {
        #region "Test Classes"



        #endregion


        [Fact]
        public void Should_Have_NotNull_List()
        {
            IList<Device<int>> devices = new List<Device<int>>();
            InMemoryRepository<Device<int>> repository = new InMemoryRepository<Device<int>>(devices);
            var query = repository.Read.ListAll();
            query.Should().NotBeNull();
        }

        [Fact]
        public void Should_Return_Repository_Count()
        {
            IList<Device<int>> devices = new List<Device<int>>();
            Device<int> device1 = new Device<int>();
            Device<int> device2 = new Device<int>();
            Device<int> device3 = new Device<int>();
            // insert the new device into the repository
            InMemoryRepository<Device<int>> repository = new InMemoryRepository<Device<int>>(devices);
            using (var scope = new UnitOfWorkScope())
            {
                repository.Write.Add(device1);
                repository.Write.Add(device2);
                repository.Write.Add(device3);
                scope.Commit();
            }
            repository.Read.GetCount().Should().Be(3);
        }

        [Fact]
        public void Return_Item_By_Specification()
        {
            ISpecification<Device<int>> specification
                = new Specification<Device<int>>(
                    x =>
                    x.Metadata.Creator.Equals("Nick Kellett") &&
                    x.Metadata.Description.Equals("Device metadata")
                );
            IList<Device<int>> devices = new List<Device<int>>();
            DomainEntityKey key = new DomainEntityKey(SequentialGuid.Generate(SequentialGuid.SequentialGuidType.SequentialAsString), "en");
            MetadataInformation meta = new MetadataInformation();
            meta.Creator = "Nick Kellett";
            meta.Description = "Device metadata";
            Device<int> device1 = new Device<int>(key, meta);
            Device<int> device2 = new Device<int>();
            Device<int> device3 = new Device<int>();
            var repository = new InMemoryRepository<Device<int>>(devices);

            // insert the new device into the repository
            using (var scope = new UnitOfWorkScope())
            {
                repository.Write.Add(device1);
                repository.Write.Add(device2);
                repository.Write.Add(device3);
                scope.Commit();
            }

            // test the query
            var returnedDevice = repository.Read.Query(specification);
            returnedDevice.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Should_Allow_Add()
        {
            IList<Device<int>> devices = new List<Device<int>>();
            DomainEntityKey key = new DomainEntityKey(SequentialGuid.Generate(SequentialGuid.SequentialGuidType.SequentialAsString), "en");
            MetadataInformation meta = new MetadataInformation();
            meta.Creator = "Nick Kellett";
            meta.Description = "Device metadata";
            Device<int> newDevice = new Device<int>(key, meta);
            var repository = new InMemoryRepository<Device<int>>(devices);

            // insert the new device into the repository
            using (var scope = new UnitOfWorkScope())
            {
                var query = repository.Write.Add(newDevice); 
                scope.Commit();
            }

            // Now we check if the repository added the item successfully
            var deviceQuery = repository.Read.GetByKey(key);
            deviceQuery.Metadata.Should().Be(meta);

        }

        [Fact]
        public void Should_Allow_Save()
        {
            IList<Device<int>> devices = new List<Device<int>>();
            DomainEntityKey key = new DomainEntityKey(SequentialGuid.Generate(SequentialGuid.SequentialGuidType.SequentialAsString), "en");
            MetadataInformation meta = new MetadataInformation();
            meta.Creator = "Nick Kellett";
            meta.Description = "Device metadata";
            Device<int> newDevice = new Device<int>(key, meta);
            newDevice.Id = 1;
            var repository = new InMemoryRepository<Device<int>>(devices);

            // insert the new device into the repository
            using (var scope = new UnitOfWorkScope())
            {
                repository.Write.Add(newDevice);
                scope.Commit();
            }

            // now we modify and save the updated device into the repository
            newDevice.Metadata.Description = "Description has changed";
            using (var scope = new UnitOfWorkScope())
            {
                repository.Write.Save(newDevice);
                scope.Commit();
            }
            // Now we check if the repository added the item successfully
            var deviceQuery = repository.Read.GetByKey(key);
            deviceQuery.Metadata.Description.Should().Be("Description has changed");

        }

        [Fact]
        public void Should_Allow_Delete()
        {
            IList<Device<int>> devices = new List<Device<int>>();
            DomainEntityKey key = new DomainEntityKey(SequentialGuid.Generate(SequentialGuid.SequentialGuidType.SequentialAsString), "en");
            MetadataInformation meta = new MetadataInformation();
            meta.Creator = "Nick Kellett";
            meta.Description = "Device metadata";
            Device<int> deviceToDelete = new Device<int>(key, meta);
            deviceToDelete.Id = 1;
            var repository = new InMemoryRepository<Device<int>>(devices);

            // insert the new device into the repository
            using (var scope = new UnitOfWorkScope())
            {
                var query = repository.Write.Add(deviceToDelete);
                scope.Commit();
            }
            // insert the new device into the repository
            using (var scope = new UnitOfWorkScope())
            {
                repository.Write.Delete(deviceToDelete);
                scope.Commit();
            }
            repository.Read.GetCount().Should().Be(0);
        }
    }
}
