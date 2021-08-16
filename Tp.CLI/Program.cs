using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tp.CLI.Args;
using Tp.CLI.Commands;
using Microsoft.Extensions.DependencyInjection;
using Tp.CLI.ProjectBuilding;

namespace Tp.CLI
{
    class Program
    {
        public static IServiceProvider serviceProvider = RegisterServices();
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                emptyCommand();
                return;
            }

            CommandLineArgs commandLineArgs = CommandLineParser.Parse(args);

            var commandSelector = serviceProvider.GetService<ICommandSelector>();
            var commandType = commandSelector.Select(commandLineArgs);

            var command = (IConsoleCommands) serviceProvider.GetRequiredService(commandType);
            await command.ExecuteAsync(commandLineArgs);
        }
        private static void emptyCommand()
        {
            var versionString = Assembly.GetEntryAssembly()
                                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                    .InformationalVersion
                                    .ToString();

            Console.WriteLine($"\n tp v{versionString}");
            Console.WriteLine("-----------------");
            Console.WriteLine("\n Usage:");
            Console.WriteLine("  tp <command> [options]");
            Console.WriteLine("\n Commands:");
            Console.WriteLine("  new <templateName> [options]");
            Console.WriteLine("  add-gateway [options]");
        }
        private static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddScoped<CommandLineArgs>();
            services.AddScoped<IProjectBuilder, ProjectBuilder>();
            services.AddScoped<ICommandSelector, CommandSelector>();

            //Commands
            services.AddScoped<NewCommand>();
            services.AddScoped<AddGatewayCommand>();


            return services.BuildServiceProvider();
        }
    }
}
