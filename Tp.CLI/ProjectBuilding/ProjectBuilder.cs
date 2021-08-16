using System;
using System.IO;
using System.Threading.Tasks;
using Tp.CLI.ProjectBuilding.Building;
using Tp.CLI.ProjectBuilding.Building.Steps;
using System.Diagnostics;
using Tp.CLI.Args;

namespace Tp.CLI.ProjectBuilding
{
    public class ProjectBuilder : IProjectBuilder
    {
        CommandLineArgs _commandLineArgs;
        public ProjectBuilder(CommandLineArgs commandLineArgs)  
        {
            _commandLineArgs = commandLineArgs;
        }
        public async Task CreateProject(ProjectBuildingContext context)
        {
            var pipeline = new ProjectBuildPipeline(context);

            string sourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $".tp\\templates\\{context.TemplateName}\\{context.TemplateVersion}");

            if (!Directory.Exists(sourcePath))
            {
                pipeline.Steps.Add(new DownloadTemplateStep());
            }

            pipeline.Steps.Add(new CopyTemplateFolderStep());


            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            if (_commandLineArgs.Command == "new")   
            {
                pipeline.Steps.Add(new CreateSolutionStep(startInfo));
            }

            pipeline.Steps.Add(new AddTemplateToSolutionStep(startInfo));

            await pipeline.Execute();

            Console.WriteLine("\n  - v" + context.TemplateVersion + "\n  - " + context.TemplateName.ToUpper() + "\n");

        }
    }
}
