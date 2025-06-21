using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Deploy.LaunchPad.Core.Geospatial.Position
{
    public interface IGeographicFeatureCollection<T> where T : IAmGeographicFeature
    {
        IReadOnlyDictionary<string, T> Features { get; }

        event Action<T> FeatureAdded;
        event Action<string> FeatureRemoved;
        event Action<T> FeatureUpdated;

        bool Add(T feature);
        void CommitChanges();
        IEnumerable<string> GetAddedIds();
        IEnumerable<string> GetRemovedIds();
        IEnumerable<string> GetUpdatedIds();
        bool Remove(string id);
        string ToJson(JsonSerializerOptions options = null);
        bool TryGet(string id, out T feature);
        bool Update(T feature);
    }
}