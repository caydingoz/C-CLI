using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.ProjectBuilding.Building
{
    public abstract class ProjectBuildPipelineStep
    {
        public abstract Task Execute(ProjectBuildingContext context);
    }
}
