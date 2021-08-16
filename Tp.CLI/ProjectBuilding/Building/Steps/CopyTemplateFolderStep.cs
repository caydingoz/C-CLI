using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.ProjectBuilding.Building.Steps
{
    public class CopyTemplateFolderStep : ProjectBuildPipelineStep
    {
        public override async Task Execute(ProjectBuildingContext context)
        {
            string sourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $".tp\\templates\\{context.TemplateName}\\{context.TemplateVersion}");
            string targetPath = Environment.CurrentDirectory;

            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath).Replace("MyProjectName", context.ProjectName));   //Rename folders
            }

            foreach (string filePath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                string fileText = File.ReadAllText(filePath);
                string newText = fileText.Replace("MyProjectName", context.ProjectName);    //Rename files
                string newPath = filePath.Replace(sourcePath, targetPath).Replace("MyProjectName", context.ProjectName);    //Rename file content
                await File.WriteAllTextAsync(newPath, newText);
            }
        }
    }
}
