using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp.CLI.Args;
using Tp.CLI.ProjectBuilding;
using Microsoft.Extensions.DependencyInjection;
using Tp.CLI.Exceptions.SolutionFileExceptions;

namespace Tp.CLI.Commands
{
    public class AddGatewayCommand : IConsoleCommands
    {
        public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            var projectName = commandLineArgs.Options.GetOrNull(Options.Name.Short, Options.Name.Long);

            if (projectName == null)
            {
                throw new Exception(
                    "Project name required!" +
                    Environment.NewLine + Environment.NewLine
                );
            }

            var version = commandLineArgs.Options.GetOrNull(Options.Version.Short, Options.Version.Long);
            var templateName = "gateway";
            var solutionFile = GetSolutionFile(commandLineArgs);

            var context = new ProjectBuildingContext(
                projectName,
                templateName,
                version,
                solutionFile
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
            sb.AppendLine("   tp add-gateway [options]");
            sb.AppendLine("");
            sb.AppendLine(" Options:");
            sb.AppendLine("   -n | --name            Name of project");
            sb.AppendLine("   -v | --version         Version of template");
            sb.AppendLine("   -s | --solution        Solution file (.sln)");
            return sb.ToString();
        }
        private string GetSolutionFile(CommandLineArgs commandLineArgs)
        {

            var foundSolutionFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.sln");

            if (foundSolutionFiles.Length == 0)
            {
                throw new SolutionFileNotFoundException("'tp add-gateway' command should be used inside a folder containing a .sln file!");
            }

            var providedSolutionFile = commandLineArgs.Options.GetOrNull(
                                            Options.Solution.Short,
                                            Options.Solution.Long);

            if (!string.IsNullOrWhiteSpace(providedSolutionFile))
            {
                if (!providedSolutionFile.Contains(".sln")) {
                    throw new InvalidSolutionNameException("Solution file must include the file extension .sln!");
                }

                foreach (var solutionFile in foundSolutionFiles)
                {
                    if (Path.GetFileName(solutionFile) == providedSolutionFile) return providedSolutionFile;
                }

                throw new SolutionFileNotFoundException(String.Format("{0} solution file not found!", providedSolutionFile));

            }

            if (foundSolutionFiles.Length == 1)
            {
                return Path.GetFileName(foundSolutionFiles[0]);
            }

            // foundSolutionFiles.Length > 1
            var sb = new StringBuilder("There are multiple solution (.sln) files in the current directory. Please specify one of the files below:");

            foreach (var foundSolutionFile in foundSolutionFiles)
            {
                sb.AppendLine("* " + foundSolutionFile);
            }

            throw new MultipleSolutionFileException(sb.ToString());
        }
        public static class Options
        {
            public static class Name
            {
                public const string Short = "n";
                public const string Long = "name";
            }
            public static class Solution
            {
                public const string Short = "s";
                public const string Long = "solution";
            }
            public static class Version
            {
                public const string Short = "v";
                public const string Long = "version";
            }
        }
    }
}
