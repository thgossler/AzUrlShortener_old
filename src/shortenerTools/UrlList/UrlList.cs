/*
```c#
Input:


Output:
    {
        "Url": "https://SOME_URL",
        "Clicks": 0,
        "PartitionKey": "d",
        "title": "Quickstart: Create your first function in Azure using Visual Studio"
        "RowKey": "doc",
        "Timestamp": "0001-01-01T00:00:00+00:00",
        "ETag": "W/\"datetime'2020-05-06T14%3A33%3A51.2639969Z'\""
    }
*/

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
    public static class UrlList
    {
        [FunctionName("UrlList")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, 
            ExecutionContext context,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed this request: {req}");

            var result = new ListResponse();
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            StorageTableHelper stgHelper = new StorageTableHelper(config["UrlDataStorage"]); 

            try
            {
               result.UrlList = await stgHelper.GetAllShortUrlEntities();
               var host = req.RequestUri.GetLeftPart(UriPartial.Authority); 
               foreach(ShortUrlEntity url in result.UrlList){
                   url.ShortUrl = Utility.GetShortUrl(host, url.Vanity);
               }
            }
            catch (Exception ex)
            {
                log.LogError(ex, "An unexpected error was encountered.");
                return req.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            // TODO: disable FUNCTIONS_V2_COMPATIBILITY_MODE in settings by ensuring async responses in all functions as required by .NET Core 3.1 and later!

            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
