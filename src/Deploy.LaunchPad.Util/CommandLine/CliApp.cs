using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{
    public sealed class CliApp
    {
        private readonly FrozenDictionary<string, ICommand> _commands;

        public CliApp(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToDictionary(c => c.Name.Full, StringComparer.OrdinalIgnoreCase).ToFrozenDictionary();
        }

        public ICommand? TryGetCommand(string name) => _commands.GetValueOrDefault(name);

        public void PrintTopLevelHelp()
        {
            Console.WriteLine("Usage: myapp <command> [options]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            foreach (var c in _commands.Values.OrderBy(c => c.Name))
            {
                Console.WriteLine($"  {c.Name,-12} {c.Description}");
            }
            Console.WriteLine();
            Console.WriteLine("Global options:");
            Console.WriteLine("  -h, --help       Show help");
            Console.WriteLine("  -v, --version    Show version");
            Console.WriteLine();
            Console.WriteLine("Use 'myapp <command> --help' for command-specific help.");
        }

        public void PrintCommandHelp(ICommand cmd)
        {
            Console.WriteLine($"Usage: myapp {cmd.Name} [options]");
            Console.WriteLine();
            Console.WriteLine(cmd.Description);
            Console.WriteLine();
            if (cmd.Options.Count == 0)
            {
                Console.WriteLine("This command has no options.");
                return;
            }

            Console.WriteLine("Options:");
            foreach (var o in cmd.Options)
            {
                var sw = o.ShortSwitch is null ? "" : $"{o.ShortSwitch}, ";
                var req = o.Required ? " (required)" : "";
                var dv = o.DefaultValue is not null ? $" [default: {o.DefaultValue}]" : "";
                var type = o.Arity == OptionArity.None ? "" : $" <{FriendlyType(o.ValueType)}>";
                Console.WriteLine($"  {sw}{o.LongSwitch}{type}{req}");
                Console.WriteLine($"      {o.Description}{dv}");
            }
        }

        private static string FriendlyType(Type t)
            => t == typeof(string) ? "string"
             : t == typeof(bool) ? "bool"
             : t == typeof(int) ? "int"
             : t == typeof(FileInfo) ? "file"
             : t.Name;
    }

}
