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

            string content = string.Empty;
            string acmeChallengeFile = rootPath + "/.well-known/acme-challenge/" + code;
            log.LogInformation($"ACME challenge file name: {acmeChallengeFile}");

            try
            {
                content = await File.ReadAllTextAsync(acmeChallengeFile);
            }
            catch (System.Exception ex)
            {
                var errorMessage = "ACME challenge file could not be read";
                log.LogError(ex, errorMessage);
                var errorResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
                errorResponse.Content = new StringContent(errorMessage, System.Text.Encoding.UTF8, "text/plain");
                return errorResponse;
            }

            var successResponse = new HttpResponseMessage(HttpStatusCode.OK);
            successResponse.Content = new StringContent(content, System.Text.Encoding.UTF8, "text/plain");
            return successResponse;
        }
    }
}
