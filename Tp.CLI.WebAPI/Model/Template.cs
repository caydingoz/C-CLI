using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tp.CLI.WebAPI.Model
{
    public class Template
    {
        public string TemplateName { get; }
        public string TemplateVersion { get; }

        public Template(string templateName, string templateVersion = null)
        {
            TemplateName = templateName;
            TemplateVersion = templateVersion;
        }

        public string GetLatestVersion()
        {
            string[] versionDirectoriesPathArr = Directory.GetDirectories($"Templates\\{TemplateName}");
            List<string> versionDirectoriesPathList = new List<string>(versionDirectoriesPathArr);
            string latestVersionPath = versionDirectoriesPathList.OrderBy(v => v).LastOrDefault();

            return Path.GetFileName(latestVersionPath);
        }
    }
}
