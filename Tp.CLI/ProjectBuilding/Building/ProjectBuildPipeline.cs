using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.ProjectBuilding.Building
{
    public class ProjectBuildPipeline
    {
        public ProjectBuildingContext Context { get; }

        public List<ProjectBuildPipelineStep> Steps { get; }

        public ProjectBuildPipeline(ProjectBuildingContext context)
        {
            Context = context;
            Steps = new List<ProjectBuildPipelineStep>();
        }

        public async Task Execute()
        {
            foreach (var step in Steps)
            {
                await step.Execute(Context);
            }
        }

    }
}
