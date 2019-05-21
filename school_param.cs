using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace greenstar_api
{
    public static class school_param
    {
        [FunctionName("school_details")]
        public static async Task<IActionResult> school_details(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "dummy")] HttpRequest req,
            ILogger log) {
                return (ActionResult)new OkObjectResult("hello func");
        }
    }
}
