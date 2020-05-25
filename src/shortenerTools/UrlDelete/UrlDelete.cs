/*
```c#
Input:
    {
         // [Required]
        "PartitionKey": "d",

         // [Required]
        "RowKey": "doc"
    }


Output:
    200 OK
    404 NOT FOUND
*/

using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Cloud5mins.domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cloud5mins.Function
{
    public static class UrlDelete
    {
        [FunctionName("UrlDelete")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, 
            ExecutionContext context,
            ClaimsPrincipal claimsPrincipal,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed this request: {req}");

            // Validation of the inputs
            if (req == null)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }

            ShortUrlEntity input = await req.Content.ReadAsAsync<ShortUrlEntity>();
            if (input == null)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }

            string callingUpn = claimsPrincipal?.Identity?.Name ?? "Admin";

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            StorageTableHelper stgHelper = new StorageTableHelper(config["UlsDataStorage"]); 

            HttpStatusCode result;
            try
            {
                result = await stgHelper.DeleteShortUrlEntity(input);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "An unexpected error was encountered.");
                return req.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return req.CreateResponse(result);
        }
    }
}
