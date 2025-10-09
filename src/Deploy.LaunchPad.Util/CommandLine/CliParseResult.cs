using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{

    /// <summary>Holds the parsed (already converted) values for a command.</summary>
    public sealed class CliParseResult
    {
        private readonly Dictionary<string, object?> _values;

        internal CliParseResult(Dictionary<string, object?> values)
            => _values = new(values, StringComparer.OrdinalIgnoreCase);

        public bool Has(string name) => _values.ContainsKey(name);

        public T Get<T>(string name)
        {
            if (!_values.TryGetValue(name, out var val))
                throw new KeyNotFoundException($"Missing option '{name}'.");
            if (val is null) return default!;
            if (val is T ok) return ok;

            // FileInfo conversion convenience
            if (typeof(T) == typeof(string) && val is FileInfo fi) return (T)(object)fi.FullName;

            throw new InvalidCastException($"Option '{name}' is of type {val.GetType().Name}, not {typeof(T).Name}.");
        }

        public T GetOrDefault<T>(string name, T defaultValue = default!)
            => Has(name) ? Get<T>(name) : defaultValue;
    }

}
