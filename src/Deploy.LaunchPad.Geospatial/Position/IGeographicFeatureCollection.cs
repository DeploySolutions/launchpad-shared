using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Deploy.LaunchPad.Geospatial.Position
{
    public interface IGeographicFeatureCollection<T> where T : IAmGeographicFeature
    {
        IReadOnlyDictionary<string, T> Features { get; }

        event Action<T> FeatureAdded;
        event Action<string> FeatureRemoved;
        event Action<T> FeatureUpdated;

        bool AddFeature(T feature);
        void CommitFeatureChanges();
        IEnumerable<string> GetAddedFeatureIds();
        IEnumerable<string> GetRemovedFeatureIds();
        IEnumerable<string> GetUpdatedFeatureIds();
        bool RemoveFeature(string id);
        string FeatureToJson(JsonSerializerOptions options = null);
        bool TryGetFeature(string id, out T feature);
        bool UpdateFeature(T feature);
    }
}