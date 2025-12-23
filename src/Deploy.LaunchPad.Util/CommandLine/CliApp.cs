using Deploy.LaunchPad.FactoryLite.CommandLine;
using Deploy.LaunchPad.Util.Helpers;
using Deploy.LaunchPad.Util.Methods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                            case JsonValueKind.Number:
                                if (jsonElement.TryGetInt32(out int intValue))
                                {
                                    argList.Add($"--{kvp.Key}");
                                    argList.Add(intValue.ToString());
                                    methodResult.AddSuccess($"Added JsonValueKind.Number (int) {intValue}");
                                }
                                else if (jsonElement.TryGetInt64(out long longValue))
                                {
                                    argList.Add($"--{kvp.Key}");
                                    argList.Add(longValue.ToString());
                                    methodResult.AddSuccess($"Added JsonValueKind.Number (long) {longValue}");
                                }
                                else if (jsonElement.TryGetDouble(out double doubleValue))
                                {
                                    argList.Add($"--{kvp.Key}");
                                    argList.Add(doubleValue.ToString());
                                    methodResult.AddSuccess($"Added JsonValueKind.Number (double) {doubleValue}");
                                }
                                else if (jsonElement.TryGetDecimal(out decimal decimalValue))
                                {
                                    argList.Add($"--{kvp.Key}");
                                    argList.Add(decimalValue.ToString());
                                    methodResult.AddSuccess($"Added JsonValueKind.Number (decimal) {decimalValue}");
                                }
                                else
                                {
                                    string message = $"Unsupported numeric type for key '{kvp.Key}'";
                                    methodResult.AddError(message);
                                    logger.Error(message);
                                    throw new ArgumentException(message);
                                }
                                break;
                            case JsonValueKind.Array:
                                // Serialize JSON objects/arrays back to string
                                argList.Add($"--{kvp.Key}");
                                argList.Add(jsonElement.GetRawText());
                                methodResult.AddSuccess($"Added JsonValueKind.String {jsonElement.GetRawText()}");
                                break;
                            case JsonValueKind.True:
                            case JsonValueKind.False:
                                argList.Add($"--{kvp.Key}");
                                argList.Add(jsonElement.GetBoolean().ToString().ToLower());
                                methodResult.AddSuccess($"Added JsonValueKind.Boolean {jsonElement.GetBoolean()}");
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

        /// <summary>
        /// Parses the arguments and determines whether to execute in batch mode or single command mode.
        /// </summary>
        public static async Task<int> ParseArgumentsAndExecuteCommand(
            string[] args,
            CliApp app,
            IServiceProvider services,
            Castle.Core.Logging.ILogger logger,
            Dictionary<string, (Type CommandType, Type ValueType)> commandTypeMap)
        {
            if (args.Length >= 2 && args[0] == "--batch")
            {
                var batchFile = args[1];
                if (!File.Exists(batchFile))
                {
                    Console.Error.WriteLine($"Batch file not found: {batchFile}");
                    return 1;
                }

                var json = await File.ReadAllTextAsync(batchFile);
                var batch = JsonSerializer.Deserialize<List<BatchCommand>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (batch == null)
                {
                    Console.Error.WriteLine("Failed to parse batch file.");
                    return 1;
                }

                return await ExecuteBatchOrSingleCommand(app, services, logger, commandTypeMap, batchCommands: batch);
            }

            if (args.Length == 0 || args is ["--help"] or ["-h"])
            {
                app.PrintTopLevelHelp();
                return 1;
            }

            if (args is ["--version"] or ["-v"])
            {
                Console.WriteLine(new AssemblyHelper().GetAssemblyVersionString());
                return 0;
            }

            string cmdName = args[0];
            Dictionary<string, object>? singleArgs = null;
            if (args.Length > 1)
            {
                singleArgs = new Dictionary<string, object>();
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].StartsWith("--"))
                    {
                        var key = args[i][2..];
                        object? value = true;
                        if (i + 1 < args.Length && !args[i + 1].StartsWith("--"))
                        {
                            value = args[i + 1];
                            i++;
                        }
                        singleArgs[key] = value;
                    }
                }
            }

            return await ExecuteBatchOrSingleCommand(app, services, logger, commandTypeMap, singleCommand: (cmdName, singleArgs));
        }

        /// <summary>
        /// Executes either a batch of commands or a single command.
        /// </summary>
        public static async Task<int> ExecuteBatchOrSingleCommand(
            CliApp app,
            IServiceProvider services,
            Castle.Core.Logging.ILogger logger,
            Dictionary<string, (Type CommandType, Type ValueType)> commandTypeMap,
            List<BatchCommand>? batchCommands = null,
            (string CommandName, Dictionary<string, object>? Args)? singleCommand = null)
        {
            if (batchCommands != null)
            {
                int overallExit = 0;
                foreach (var entry in batchCommands)
                {
                    var exit = await app.ExecuteCommand(app, services, logger, commandTypeMap, entry.command, entry.args);
                    if (!exit.Succeeded) overallExit = 1;
                }
                return overallExit;
            }

            if (singleCommand.HasValue)
            {
                var (cmdName, args) = singleCommand.Value;
                var result = await app.ExecuteCommand(app, services, logger, commandTypeMap, cmdName, args);
                return result is not null && result.Succeeded ? 0 : 1;
            }

            throw new InvalidOperationException("Neither batch commands nor a single command were provided.");
        }


        public static async Task<Serilog.ILogger> ConfigureLogging(string serilogFilePath = "serilog.json")
        {
            var loggerConfiguration = new ConfigurationBuilder().AddJsonFile(serilogFilePath).Build();

            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif

            .WriteTo.Async(c => c.Console())
            .ReadFrom.Configuration(loggerConfiguration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .CreateLogger();
            return Log.Logger;
        }

        public static Dictionary<string, (Type CommandType, Type ValueType)> RegisterCommands(IServiceCollection services, Dictionary<string, (Type CommandType, Type ValueType)> commandTypeMap, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0)
            {
                assemblies = new[] { Assembly.GetExecutingAssembly() };
            }

            // Loop through each provided assembly
            foreach (var assembly in assemblies)
            {
                // Automatically register all ICommand implementations as transient
                var commandInterfaceType = typeof(ICommand);
                var commandTypes = assembly.GetTypes()
                .Where(t => commandInterfaceType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

                foreach (var commandType in commandTypes)
                {

                    //IocManager.Instance.RegisterIfNot(commandType, DependencyLifeStyle.Transient);
                    //IocManager.Instance.Register(commandInterfaceType, commandType, DependencyLifeStyle.Transient);
                    // Register the command type as both its concrete type and as ICommand
                    services.AddTransient(commandType); // Register the concrete type
                    services.AddTransient(commandInterfaceType, commandType); // Register as ICommand
                }


                foreach (var commandType in commandTypes)
                {
                    var valueTypeAttribute = commandType.GetCustomAttribute<CommandValueTypeAttribute>();
                    if (valueTypeAttribute != null)
                    {
                        var commandName = commandType.Name.EndsWith("Command")
                            ? commandType.Name[..^"Command".Length]
                            : commandType.Name;

                        // Ensure no duplicate entries in the commandTypeMap
                        if (!commandTypeMap.ContainsKey(commandName))
                        {
                            commandTypeMap[commandName] = (commandType, valueTypeAttribute.ValueType);
                        }
                    }
                }
            }
            return commandTypeMap;
        }
    }


}
