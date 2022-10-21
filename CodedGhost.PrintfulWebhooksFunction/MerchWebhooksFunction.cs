using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PrintfulLib.Models.WebhookResponses;

namespace CodedGhost.PrintfulWebhooksFunction
{
    public class MerchWebhooksFunction
    {
        private readonly ILogger _logger;

        public MerchWebhooksFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MerchWebhooksFunction>();
        }

        [Function("MerchWebhooksFunction")]
        [ServiceBusOutput("merch-webhook-queue", ServiceBusEntityType.Queue, Connection = "ServiceBusConnectionString")]
        public async Task<string> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestData req)
        {
            var webhookContent = await req.Body.DeserializeFromStream<PrintfulWebhookResponse>();

            if (webhookContent == null)
            {
                _logger.LogError("Received an empty payload");
                throw new Exception("Received an empty payload");
            }

            return JsonConvert.SerializeObject(webhookContent);
        }
    }
}
