using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cloud5mins.domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cloud5mins.Function
{
    public static class UrlRedirect
    {
        [FunctionName("UrlRedirect")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "UrlRedirect/{shortUrl}")] HttpRequestMessage req,
            string shortUrl, 
            ExecutionContext context,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed for Url: {shortUrl}");

            string redirectUrl = "https://azure.com";

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var redirectDomain = config["redirectDomain"];
            if (!string.IsNullOrWhiteSpace(redirectDomain))
            {
                redirectUrl = $"https://{redirectDomain}/{shortUrl ?? string.Empty}";
                var result = req.CreateResponse(HttpStatusCode.Redirect);
                result.Headers.Add("Location", redirectUrl);
                return result;
            }

            if (!String.IsNullOrWhiteSpace(shortUrl))
            {
                redirectUrl = config["defaultRedirectUrl"];

                StorageTableHelper stgHelper = new StorageTableHelper(config["UrlDataStorage"]);

                var tempUrl = new ShortUrlEntity(string.Empty, string.Empty, shortUrl);

                var newUrl = await stgHelper.GetShortUrlEntity(tempUrl);

                if (newUrl != null)
                {
                    log.LogInformation($"Found it: {newUrl.Url}");
                    newUrl.Clicks++;
                    stgHelper.SaveClickStatsEntity(new ClickStatsEntity(newUrl.RowKey));
                    await stgHelper.SaveShortUrlEntity(newUrl);
                    redirectUrl = WebUtility.UrlDecode(newUrl.Url);
                }
            }
            else
            {
                log.LogInformation("Bad Link, resorting to fallback.");
            }

            var res = req.CreateResponse(HttpStatusCode.Redirect);
            res.Headers.Add("Location", redirectUrl);
            return res;
        }
  }
}
