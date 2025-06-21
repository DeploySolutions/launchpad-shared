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
        protected readonly HashSet<string> _added = new();
        protected readonly HashSet<string> _updated = new();
        protected readonly HashSet<string> _removed = new();

        // Events
        public event Action<T>? FeatureAdded;
        public event Action<T>? FeatureUpdated;
        public event Action<string>? FeatureRemoved;


        protected GeographicFeatureCollection() : base()
        { 
        }


        public virtual bool Add(T feature)
        {
            if (feature == null || string.IsNullOrWhiteSpace(feature.Key) || string.IsNullOrEmpty(feature.Key) || _features.ContainsKey(feature.Key))
                return false;

            _features.TryAdd(feature.Key, feature);
            _added.Add(feature.Key);
            _removed.Remove(feature.Key); // undo removal
            FeatureAdded?.Invoke(feature);
            return true;
        }

        public virtual bool Remove(string id)
        {
            if (_features.TryGetValue(id, out var removedFeature))
            {
                _features.Remove(id);
                _removed.Add(id);
                _added.Remove(id); // undo add
                _updated.Remove(id); // undo update
                FeatureRemoved?.Invoke(id);
                return true;
            }
            return false;
        }

        public virtual bool Update(T feature)
        {
            if (feature == null || string.IsNullOrWhiteSpace(feature.Key) || string.IsNullOrEmpty(feature.Key) || !_features.ContainsKey(feature.Key))
                return false;

            _features.TryAdd(feature.Key, feature);
            if (!_added.Contains(feature.Key)) // Don't track updates for new features
                _updated.Add(feature.Key);

            FeatureUpdated?.Invoke(feature);
            return true;
        }

        public virtual bool TryGet(string id, out T feature)
        {
            return _features.TryGetValue(id, out feature);
        }

        // Serialization (override this if needed)
        public virtual string ToJson(JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(_features.Values, options ?? new JsonSerializerOptions { WriteIndented = true });
        }

        // Change tracking access
        public virtual IEnumerable<string> GetAddedIds() => _added;
        public virtual IEnumerable<string> GetUpdatedIds() => _updated;
        public virtual IEnumerable<string> GetRemovedIds() => _removed;

        public virtual void CommitChanges()
        {
            _added.Clear();
            _updated.Clear();
            _removed.Clear();
        }
    }
}
