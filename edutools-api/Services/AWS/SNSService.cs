using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using Newtonsoft.Json;
using System.Net;

namespace edutools_api.Services.AWS
{
    public class SNSService : ISNSService
    {
        public SNSService()
        {

        }

        public async Task<bool> EmitSNSEvent(string type, object detail)
        {
            var client = new AmazonEventBridgeClient();

            var message = new PutEventsRequestEntry
            {
                Detail = JsonConvert.SerializeObject(detail),
                DetailType = type,
                EventBusName = "default",
                Source = "EduTools.API"
            };

            var putRequest = new PutEventsRequest
            {
                Entries = new List<PutEventsRequestEntry> { message }
            };

            var response = await client.PutEventsAsync(putRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}
