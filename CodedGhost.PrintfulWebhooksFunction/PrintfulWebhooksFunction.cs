using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PrintfulLib.Models.WebhookResponses;

namespace CodedGhost.PrintfulWebhooksFunction
{
    public class PrintfulWebhooksFunction
    {
        private readonly ILogger _logger;

        public PrintfulWebhooksFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PrintfulWebhooksFunction>();
        }

        [Function("PrintfulWebhooksFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var webhookContent = await req.Body.DeserializeFromStream<PrintfulWebhookResponse>();

            _logger.LogInformation(JsonConvert.SerializeObject(webhookContent));

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
