using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    public partial class GenericFileProvider
    {        
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ILogger Logger { get; set; } = NullLogger.Instance;
        public virtual string Description { get; set; } = String.Empty;
        public virtual Dictionary<string, IFileStorageLocation> Locations { get; set; }

        public virtual IFileStorageLocation GetLocationById(string id, string caller)
        {
            return GetLocationByIdAsync(id, caller).Result;   
        }

        public virtual Task<IFileStorageLocation> GetLocationByIdAsync(string id, string caller ="")
        {
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(id), "id cannot be null or empty.");
            Task<IFileStorageLocation> location = null;
            if(Locations.ContainsKey(id))
            {
                location = Task.FromResult(Locations[id]);
            }
            return location;
        }

        public GenericFileProvider()
        {
            Id = Guid.NewGuid().ToString();
            Name = "File Provider " + Id;
            Locations = new Dictionary<string, IFileStorageLocation>();
        }

        public GenericFileProvider(ILogger logger)
        {
            Id = Guid.NewGuid().ToString();
            Name = "File Provider " + Id;
            Locations = new Dictionary<string, IFileStorageLocation>();
            if(logger != null)
            {
                Logger = logger;
            }
        }

        public GenericFileProvider(ILogger logger, string id, string name)
        {
            Id = id;
            Name = name;
            Locations = new Dictionary<string, IFileStorageLocation>();
            if (logger != null)
            {
                Logger = logger;
            }
        }
    }
}