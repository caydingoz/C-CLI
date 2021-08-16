using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tp.CLI.Exceptions.APIExceptions;

namespace Tp.CLI.ProjectBuilding
{
    public static class GetTemplateLatestVersion
    {
        public static async Task<string> GetLatestVersion(string templateName)
        {
            var values = new Dictionary<string, string>
                {
                    { "templateName", templateName }
                };
            string jsonString = JsonSerializer.Serialize(values);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("https://localhost:5001/api/templates/version", httpContent);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestFailedException(
                        response.Content.ReadAsStringAsync().Result +
                        Environment.NewLine + Environment.NewLine
                    );
                }
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}
