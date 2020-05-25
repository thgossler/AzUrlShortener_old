// https://YOURWEBSITE.azurewebsites.net/api/letsencrypt/{code}

using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Cloud5mins.Function
{
    public static class LetsEncrypt
    {
        [FunctionName("LetsEncrypt")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "LetsEncrypt/{code}")] HttpRequestMessage req,
            string code, ExecutionContext context, ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed a request. {code}");

            string rootPath = context.FunctionAppDirectory;
            var content = await File.ReadAllTextAsync(rootPath + @".well-known\acme-challenge\" + code);

            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent(content, System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }
    }
}
