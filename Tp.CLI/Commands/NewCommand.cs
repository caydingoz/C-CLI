using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp.CLI.Args;
using Tp.CLI.ProjectBuilding;
using Microsoft.Extensions.DependencyInjection;
using Tp.CLI.Exceptions.SolutionFileExceptions;

namespace Tp.CLI.Commands
{
    public class NewCommand : IConsoleCommands
    {
        public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            if (commandLineArgs.TemplateName == null)
            {
                throw new Exception(
                    "Template name is missing!" +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }

            var projectName = commandLineArgs.Options.GetOrNull(Options.Name.Short, Options.Name.Long);

            if (projectName == null)
            {
                throw new Exception(
                    "Project name required!" +
                    Environment.NewLine + Environment.NewLine
                );
            }

            var version = commandLineArgs.Options.GetOrNull(Options.Version.Short, Options.Version.Long);
            var solutionName = commandLineArgs.Options.GetOrNull(Options.Solution.Short, Options.Solution.Long);
            var templateName = commandLineArgs.TemplateName.ToLower();

            if (solutionName == null)
            {
                solutionName = projectName + ".sln";
            }

            if(!solutionName.Contains(".sln")) {
                throw new InvalidSolutionNameException("Solution file must include the file extension .sln!");
            }

            var context = new ProjectBuildingContext(
                projectName,
                templateName,
                version,
                solutionName
            );

            Console.WriteLine("\n\n Creating your project...");

            var projectBuilder = Program.serviceProvider.GetService<IProjectBuilder>();
            await projectBuilder.CreateProject(context);

            Console.WriteLine(" Your project created.");
        }
        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine(" Usage:");
            sb.AppendLine("   tp new <project-type> [options]");
            sb.AppendLine("");
            sb.AppendLine(" Templates:");
            sb.AppendLine("   webapi                 ASP.NET Core Web API");
            sb.AppendLine("   console                Console Application");
            sb.AppendLine("");
            sb.AppendLine(" Options:");
            sb.AppendLine("   -n | --name            Name of project");
            sb.AppendLine("   -v | --version         Version of template");
            sb.AppendLine("   -s | --solution        Solution file (.sln)");
            return sb.ToString();
        }
        public static class Options
        {
            public static class Name
            {
                public const string Short = "n";
                public const string Long = "name";
            }
            public static class Version
            {
                public const string Short = "v";
                public const string Long = "version";
            }
            public static class Solution
            {
                public const string Short = "s";
                public const string Long = "solution";
            }
        }
    }
}
