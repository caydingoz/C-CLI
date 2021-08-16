using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tp.CLI.ProjectBuilding.Building.Steps
{
    public class CreateSolutionStep : ProjectBuildPipelineStep
    {
        private ProcessStartInfo _processInfo;
        public CreateSolutionStep(ProcessStartInfo processInfo)
        {
            _processInfo = processInfo;
        }
        public override async Task Execute(ProjectBuildingContext context)
        {
            using (Process process = new Process())
            {
                process.StartInfo = _processInfo;

                process.EnableRaisingEvents = true;

                string solutionName = context.SolutionName;

                solutionName = solutionName.Replace(".sln", "");

                _processInfo.Arguments = $"/C dotnet new sln -n {solutionName}";

                process.Start();
                await process.WaitForExitAsync().ConfigureAwait(true);
            } 

        }
    }
}
