using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.ProjectBuilding.Building.Steps
{
    public class DownloadTemplateStep : ProjectBuildPipelineStep
    {
        public override async Task Execute(ProjectBuildingContext context)
        {
            await DownloadTemplateSource.DownloadTemplate(context.TemplateName, context.TemplateVersion);
        }
    }
}
