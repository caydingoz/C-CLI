using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tp.CLI.Exceptions.APIExceptions;
using System.Configuration;

namespace Tp.CLI.ProjectBuilding
{
    public static class DownloadTemplateSource
    {
        public static async Task DownloadTemplate(string templateName, string templateVersion)
        {
            var values = new Dictionary<string, string>
                {
                    { "templateName", templateName },
                    { "templateVersion", templateVersion }
                };
            string jsonString = JsonSerializer.Serialize(values);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("https://localhost:5001/api/templates", httpContent);
                if(response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestFailedException(
                        response.Content.ReadAsStringAsync().Result +
                        Environment.NewLine + Environment.NewLine
                    );
                }

                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $".tp\\templates\\{templateName}");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $".tp\\templates\\{templateName}\\v{templateVersion}");
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var fileInfo = new FileInfo(downloadPath);
                    using (var fileStream = fileInfo.OpenWrite())
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }
                //Extract from zip file
                string extractPath = Path.Combine(path, templateVersion);
                Directory.CreateDirectory(extractPath);
                ZipFile.ExtractToDirectory(downloadPath , extractPath);

                //Delete zip file
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    if(Path.GetFileName(file) == "v" + templateVersion)
                    {
                        File.Delete(file);
                    }
                }
            }

        }
    }
}
