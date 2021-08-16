using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tp.CLI.ProjectBuilding.Building.Steps
{
    public class AddTemplateToSolutionStep : ProjectBuildPipelineStep
    {
        private ProcessStartInfo _processInfo;
        public AddTemplateToSolutionStep(ProcessStartInfo processInfo)
        {
            _processInfo = processInfo;
        }
        public override async Task Execute(ProjectBuildingContext context)
        {
            using (Process process = new Process())
            {
                process.StartInfo = _processInfo;

                process.EnableRaisingEvents = true;

                _processInfo.Arguments = $"/C dotnet sln {context.SolutionName} add {context.ProjectName}/{context.ProjectName}.csproj";

                process.Start();
                await process.WaitForExitAsync().ConfigureAwait(true);
            }

        }
    }
}
