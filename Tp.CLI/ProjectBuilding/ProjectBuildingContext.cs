using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.ProjectBuilding
{
    public class ProjectBuildingContext
    {
        public string ProjectName { get; }
        public string TemplateName { get; }
        public string TemplateVersion { get; }
        public string SolutionName { get; }

        public ProjectBuildingContext(string projectName, string templateName, string templateVersion, string solutionName)
        {
            ProjectName = projectName;
            TemplateName = templateName;
            TemplateVersion = templateVersion ?? SetVersionToLatest().Result;
            SolutionName = solutionName;
        }

        private async Task<string> SetVersionToLatest()
        {
            return await GetTemplateLatestVersion.GetLatestVersion(TemplateName);
        }
    }
}
