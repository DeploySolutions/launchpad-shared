using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{
    public static class CliParser
    {
        /// <summary>Parse argv for a specific command.</summary>
        public static CliParseResult Parse(ICliCommand command, string[] argv, out string? error)
        {
            error = null;

            var defs = command.Options.ToDictionary(o => o.Name, StringComparer.OrdinalIgnoreCase);
            var byLong = command.Options.ToDictionary(o => o.LongSwitch, StringComparer.OrdinalIgnoreCase);
            var byShort = command.Options
                .Where(o => o.ShortSwitch is not null)
                .ToDictionary(o => o.ShortSwitch!, StringComparer.OrdinalIgnoreCase);

            var values = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

            // cursor-based scan
            for (int i = 0; i < argv.Length; i++)
            {
                var token = argv[i];

                // --foo=bar
                if (token.StartsWith("--", StringComparison.Ordinal))
                {
                    string key, maybeVal = "";
                    var eqIdx = token.IndexOf('=', StringComparison.Ordinal);
                    if (eqIdx >= 0)
                    {
                        key = token[..eqIdx];
                        maybeVal = token[(eqIdx + 1)..];
                    }
                    else key = token;

                    if (!byLong.TryGetValue(key, out var def))
                    {
                        error = $"Unknown option '{key}'.";
                        return new(values);
                    }

                    if (def.Arity == OptionArity.None)
                    {
                        // boolean flag; allow explicit "=false"
                        bool val = true;
                        if (eqIdx >= 0 && bool.TryParse(maybeVal, out var b)) val = b;
                        values[def.Name] = val;
                    }
                    else
                    {
                        string? val;
                        if (eqIdx >= 0)
                        {
                            val = maybeVal;
                        }
                        else
                        {
                            if (i + 1 >= argv.Length)
                            {
                                error = $"Option '{def.LongSwitch}' requires a value.";
                                return new(values);
                            }
                            val = argv[++i];
                        }

                        if (!TryConvert(val, def.ValueType, out var converted, out var convErr))
                        {
                            error = $"Invalid value for '{def.LongSwitch}': {convErr}";
                            return new(values);
                        }
                        values[def.Name] = converted;
                    }
                    continue;
                }

                // -n foo or -n=foo or -y
                if (token.StartsWith("-", StringComparison.Ordinal))
                {
                    var eqIdx = token.IndexOf('=', StringComparison.Ordinal);
                    var key = eqIdx >= 0 ? token[..eqIdx] : token;

                    if (!byShort.TryGetValue(key, out var def))
                    {
                        error = $"Unknown option '{key}'.";
                        return new(values);
                    }

                    if (def.Arity == OptionArity.None)
                    {
                        bool val = true;
                        if (eqIdx >= 0 && bool.TryParse(token[(eqIdx + 1)..], out var b)) val = b;
                        values[def.Name] = val;
                    }
                    else
                    {
                        string? val;
                        if (eqIdx >= 0)
                        {
                            val = token[(eqIdx + 1)..];
                        }
                        else
                        {
                            if (i + 1 >= argv.Length)
                            {
                                error = $"Option '{def.ShortSwitch}' requires a value.";
                                return new(values);
                            }
                            val = argv[++i];
                        }

                        if (!TryConvert(val, def.ValueType, out var converted, out var convErr))
                        {
                            error = $"Invalid value for '{def.ShortSwitch}': {convErr}";
                            return new(values);
                        }
                        values[def.Name] = converted;
                    }
                    continue;
                }

                // Positional arguments (if you want them) — not used in the sample.
                error = $"Unexpected argument '{token}'. Use --help for usage.";
                return new(values);
            }

            // Fill defaults & check requireds
            foreach (var def in command.Options)
            {
                if (!values.ContainsKey(def.Name))
                {
                    if (def.Required)
                    {
                        error = $"Missing required option '{def.LongSwitch}'.";
                        return new(values);
                    }
                    if (def.Arity == OptionArity.None && def.ValueType == typeof(bool))
                    {
                        // flags default to false unless a default was provided
                        values[def.Name] = def.DefaultValue ?? false;
                    }
                    else if (def.DefaultValue is not null)
                    {
                        values[def.Name] = def.DefaultValue;
                    }
                }
            }

            return new(values);
        }

        private static bool TryConvert(string? input, Type target, out object? value, out string? error)
        {
            error = null;
            value = null;

            if (target == typeof(string))
            {
                value = input ?? "";
                return true;
            }
            if (target == typeof(bool))
            {
                if (bool.TryParse(input, out var b)) { value = b; return true; }
                error = "expected 'true' or 'false'";
                return false;
            }
            if (target == typeof(int))
            {
                if (int.TryParse(input, out var n)) { value = n; return true; }
                error = "expected an integer";
                return false;
            }
            if (target == typeof(FileInfo))
            {
                if (string.IsNullOrWhiteSpace(input)) { error = "expected a file path"; return false; }
                value = new FileInfo(input);
                return true;
            }

            // Extend with more primitives as needed (double, DateTime, etc.)
            error = $"unsupported type {target.Name}";
            return false;
        }
    }

}
