using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Geospatial.Position
{
    public abstract partial class GeographicFeatureCollection<T> : IGeographicFeatureCollection<T> 
        where T : IAmGeographicFeature
    {
        private readonly Dictionary<string, T> _features = new();

        // Exposed as read-only
        public virtual IReadOnlyDictionary<string, T> Features => _features;

        // Change tracking sets
        protected readonly HashSet<string> _featureAdded = new();
        protected readonly HashSet<string> _featureUpdated = new();
        protected readonly HashSet<string> _featureRemoved = new();

        // Events
        public event Action<T>? FeatureAdded;
        public event Action<T>? FeatureUpdated;
        public event Action<string>? FeatureRemoved;


        protected GeographicFeatureCollection() : base()
        { 
        }


        public virtual bool AddFeature(T feature)
        {
            if (feature == null || string.IsNullOrWhiteSpace(feature.Key) || string.IsNullOrEmpty(feature.Key) || _features.ContainsKey(feature.Key))
                return false;

            _features.TryAdd(feature.Key, feature);
            _featureAdded.Add(feature.Key);
            _featureRemoved.Remove(feature.Key); // undo removal
            FeatureAdded?.Invoke(feature);
            return true;
        }

        public virtual bool RemoveFeature(string featureKey)
        {
            if (_features.TryGetValue(featureKey, out var removedFeature))
            {
                _features.Remove(featureKey);
                _featureRemoved.Add(featureKey);
                _featureAdded.Remove(featureKey); // undo add
                _featureUpdated.Remove(featureKey); // undo update
                FeatureRemoved?.Invoke(featureKey);
                return true;
            }
            return false;
        }

        public virtual bool UpdateFeature(T feature)
        {
            if (feature == null || string.IsNullOrWhiteSpace(feature.Key) || string.IsNullOrEmpty(feature.Key) || !_features.ContainsKey(feature.Key))
                return false;

            _features.TryAdd(feature.Key, feature);
            if (!_featureAdded.Contains(feature.Key)) // Don't track updates for new features
                _featureUpdated.Add(feature.Key);

            FeatureUpdated?.Invoke(feature);
            return true;
        }

        public virtual bool TryGetFeature(string featureKey, out T feature)
        {
            return _features.TryGetValue(featureKey, out feature);
        }

        // Serialization (override this if needed)
        public virtual string FeatureToJson(JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(_features.Values, options ?? new JsonSerializerOptions { WriteIndented = true });
        }

        // Change tracking access
        public virtual IEnumerable<string> GetAddedFeatureIds() => _featureAdded;
        public virtual IEnumerable<string> GetUpdatedFeatureIds() => _featureUpdated;
        public virtual IEnumerable<string> GetRemovedFeatureIds() => _featureRemoved;

        public virtual void CommitFeatureChanges()
        {
            _featureAdded.Clear();
            _featureUpdated.Clear();
            _featureRemoved.Clear();
        }
    }
}
