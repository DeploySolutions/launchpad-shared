using Castle.Core.Logging;
using Deploy.LaunchPad.Util.Methods;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.CommandLine
{
    public sealed partial class CliAppResultValue : LaunchPadMethodResultValueBase, ILaunchPadMethodResultValue
    { 

    }

    public sealed class CliApp
    {
        private readonly FrozenDictionary<string, ICommand> _commands;

        public CliApp(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToDictionary(c => c.Name.Full, StringComparer.OrdinalIgnoreCase).ToFrozenDictionary();
        }

        public ICommand? TryGetCommand(string name) => _commands.GetValueOrDefault(name);

        // Helper to execute a command by name and args
        public async Task<ICommandResult> ExecuteCommand(CliApp app, IServiceProvider serviceProvider, Castle.Core.Logging.ILogger logger, Dictionary<string,(Type CommandType, Type ValueType)> commandTypeMap, string commandName, Dictionary<string, object>? argDict)
        {
            ICommandResult methodResult = new CommandResult<CliAppResultValue>(new CliAppResultValue());
            if (!commandTypeMap.TryGetValue(commandName, out var types))
            {
                string errorMessage = $"Unknown command '{commandName}'.";
                methodResult.AddError(errorMessage);
                logger.Error(errorMessage);
                app.PrintTopLevelHelp();
                return methodResult;
            }

            var (commandType, valueType) = types;
            var command = app.TryGetCommand(commandName);
            if (command == null)
            {
                string errorMessage = $"Unknown command '{commandName}'.";
                methodResult.AddError(errorMessage);
                logger.Error(errorMessage);
                return methodResult;
            }

            var method = command.GetType().GetMethod("ExecuteAsync");
            var genericMethod = method.MakeGenericMethod(commandType, valueType);

            var argList = new List<string>();
            if (argDict != null)
            {
                foreach (var kvp in argDict)
                {
                    Console.WriteLine($"Key: {kvp.Key}, Value Type: {kvp.Value?.GetType()}, Value: {kvp.Value}");
                    if (kvp.Value is JsonElement jsonElement)
                    {
                        // Handle JsonElement based on its type
                        switch (jsonElement.ValueKind)
                        {
                            case JsonValueKind.String:
                                argList.Add($"--{kvp.Key}");
                                argList.Add(jsonElement.GetString());
                                methodResult.AddSuccess($"Added JsonValueKind.String {jsonElement.GetString()}");
                                break;
                            case JsonValueKind.Object:
                            case JsonValueKind.Array:
                                // Serialize JSON objects/arrays back to string
                                argList.Add($"--{kvp.Key}");
                                argList.Add(jsonElement.GetRawText());
                                methodResult.AddSuccess($"Added JsonValueKind.String {jsonElement.GetRawText()}");
                                break;
                            default:
                                string errorMessage = $"Unsupported JsonElement type for key '{kvp.Key}': {jsonElement.ValueKind}";
                                methodResult.AddError(errorMessage);
                                logger.Error(errorMessage);
                                throw new ArgumentException(errorMessage);
                        }
                    }
                    else if (kvp.Value is bool b && b)
                    {
                        argList.Add($"--{kvp.Key}");
                        methodResult.AddSuccess($"Added {kvp.Key}");
                    }
                    else if (kvp.Value is string strValue)
                    {
                        // Directly add string values (e.g., file paths)
                        argList.Add($"--{kvp.Key}");
                        argList.Add(strValue);
                        methodResult.AddSuccess($"Added {strValue}");

                    }
                    else if (kvp.Value is not null && !(kvp.Value.GetType().IsPrimitive))
                    {
                        // Serialize complex objects to JSON strings
                        string jsonValue = JsonSerializer.Serialize(kvp.Value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        argList.Add($"--{kvp.Key}");
                        argList.Add(jsonValue);
                        methodResult.AddSuccess($"Added {kvp.Key}");
                    }
                    else
                    {
                        argList.Add($"--{kvp.Key}");
                        argList.Add(kvp.Value?.ToString() ?? "");
                        methodResult.AddSuccess($"Added {kvp.Key}");
                    }
                }
            }

            var parseResult = CommandArgsParser.Parse(command, argList.ToArray());
            if (parseResult != null && !parseResult.Succeeded)
            {
                string errorMessage = $"Error in parse of command '{commandName}': {parseResult.Errors}";
                methodResult.AddError(errorMessage);
                logger.Error(errorMessage);
                throw new ArgumentException(errorMessage);
            }
            CommandInput input = new CommandInput
            {
                Logger = logger,
                Args = parseResult.UnderlyingResult.Value,
                Services = serviceProvider,
                Ct = CancellationToken.None
            };
            var task = (Task)genericMethod.Invoke(command, new object[] { input });
            await task.ConfigureAwait(false);

            var resultProperty = task.GetType().GetProperty("Result");
            var newResult = (ICommandResult)resultProperty.GetValue(task);

            // Merge success messages
            foreach (var successMessage in newResult.Successes.Values)
            {
                methodResult.AddSuccess(successMessage);
            }

            // Merge error messages
            foreach (var errorMessage in newResult.Errors.Values)
            {
                methodResult.AddError(errorMessage);
            }
            return methodResult;
        }


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
