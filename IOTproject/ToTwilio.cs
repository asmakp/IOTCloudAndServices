using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Twilio;


namespace IOTproject
{
    public static class ToTwilio
    {
        [FunctionName("ToTwilio")]
        public static void Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
             ILogger log)
        {
            log.LogInformation("Twilio processed a request.");
            string data = new StreamReader(req.Body).ReadToEnd();

           
            string accountSid = "AC357a3abd1d86e3e406b7de8e8b25f68b";
            string authToken = "d63dfe50b8ea5c0ca3cbd9bf46b83b2d";

            
            var client = new TwilioRestClient(accountSid, authToken);
            client.SendMessage("+19894871408", "+46727602028", data + DateTime.Now);
           

        }
    }
}
