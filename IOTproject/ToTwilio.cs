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


            string accountSid = "";
            string authToken = "";

           /* DateTime today = DateTime.Now;   //present date and time
            double hour = 1;                 // to increment the hour
            int result;
            variable to comare time 

            DateTime expectedanswer;  //adding one hour to todays date and time
            do
            {
                client.SendMessage("+19894871408", "+46727602028", data + DateTime.Now);
                expectedanswer = today.AddHours(hour);
                result = DateTime.Compare(today, expectedanswer); // comparing the time after adding one hour to the current time
            } while (result == 0);*/



            var client = new TwilioRestClient(accountSid, authToken);
            client.SendMessage("twilioNo", "MyNumber", data + DateTime.Now);
           


        }
    }
}
