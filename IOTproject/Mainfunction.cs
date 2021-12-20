using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using SmhiWeather;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IOTproject.Model;
using System;

namespace IOTproject
{
    public static class Mainfunction
    {
        private static HttpClient client = new HttpClient();

        public static ISmhi smhi = new Smhi(59.2136m, 18.003m);
        public static SmhiWeather.ForecastTimeSerie CurrentWeather = smhi.GetCurrentWeather();

        public static float temp = (float)CurrentWeather.Temperature;
        public static float humid = CurrentWeather.RelativeHumidity;
        public static int precipitation = (int)CurrentWeather.PrecipitationCategory;
       

        [FunctionName("Mainfunction")]
        public static async Task Run([IoTHubTrigger("messages/events", Connection = "IotHubConnection", ConsumerGroup = "storage")] EventData message, ILogger log)
        {
           

        log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");


            var _data = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(message.Body.Array));
            var _moisture = _data.PercentMoisture;
            var _MsgCreatedtime = _data.MsgCreateTime;
            int _prevmoisture = 0;


            //DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(_data.MsgCreateTime.ToString()));
            // DateTime _measurementTime = dateTimeOffset.DateTime;


            var _endResult = new dataHandle()
            {
                Temparature = Mainfunction.temp,
                Humididty = Mainfunction.humid,
                Precipitation = Mainfunction.precipitation,
                Moisture = _moisture,
                MsgCreatedtime = _MsgCreatedtime,
            };
           

            string endResultjson = JsonConvert.SerializeObject(_endResult); //Serializing the object created

            log.LogInformation($"the data is  saved now to jsondoc format , Message:: {endResultjson}");

            HttpContent payload = new StringContent(endResultjson, Encoding.UTF8, "application/json"); // converting the serialized to string Httpcontent

            //  HttpContent payload = new StringContent(Encoding.UTF8.GetString(message.Body.Array), Encoding.UTF8, "application/json");
           
        


            //to check for change in value and then send to cosmosdb
            if (_moisture > _prevmoisture || _moisture < _prevmoisture)
            {
                _prevmoisture = _moisture;

                // sending data to cosmos DB - Fetched the url from azure cloud  -> Function App -> Get Function URL
                await client.PostAsync(new Uri(Environment.GetEnvironmentVariable("CosmosDbUrl")), payload);

                // await client.PostAsJsonAsync($"https://iot20-func.azurewebsites.net/api/ToCosmosDB?code=bW1e/dBg4TaLjk7Yw3hXNACdysp9BhekBnfYN1/baBQ180DlL9mxZg==", endResultjson);
            }

            //sending data to blobstorage - Fetched the url from azure cloud  -> Function App -> Get Function URL
            await client.PostAsync(new Uri(Environment.GetEnvironmentVariable("BlobStorageUrl")), payload);

            //  await client.PostAsJsonAsync($"https://iot20-func.azurewebsites.net/api/ToBlobStorage?code=iIb2f57U6jeooRvNLsNSggGUycUhsNL6weWaaG3Yk4RnmAkd3dzuow==", endResultjson);



            //sending data to twilio - Fetched the url from azure cloud  -> Function App -> Get Function URL
            if (_moisture < 25)
            {
                if (precipitation == 3 || precipitation == 4)
                {
                    var _endSmsResult = new smsData()
                    {
                        message = "Water plants",
                        weather = "rain or drizzle",
                    };

                    var endSmsResultjson = JsonConvert.SerializeObject(_endSmsResult);
                    HttpContent Smspayload = new StringContent(endSmsResultjson, Encoding.UTF8, "application/json");
                    await client.PostAsync(new Uri(Environment.GetEnvironmentVariable("TwolioUrl")), Smspayload);
                    //  await client.PostAsJsonAsync($"https://iot20-func.azurewebsites.net/api/ToTwilio?code=xnDdAaUj0SbXikIFDoQISt6Do50o1N1MucI7gG1JQcKS/poJKfvWjg==", endSmsResultjson);
                    //  log.LogInformation($"the data is  saved to jsondoc format , Message:: {endSmsResultjson}");
                }
                else
                {
                    var _endSmsResult = new smsData()
                    {
                        message = "Water plants",
                        weather = "No rain or drizzle",
                    };

                    var endSmsResultjson = JsonConvert.SerializeObject(_endSmsResult);
                    HttpContent Smspayload = new StringContent(endSmsResultjson, Encoding.UTF8, "application/json");
                    await client.PostAsync(new Uri(Environment.GetEnvironmentVariable("TwolioUrl")), Smspayload);

                    // await client.PostAsJsonAsync($"https://iot20-func.azurewebsites.net/api/ToTwilio?code=xnDdAaUj0SbXikIFDoQISt6Do50o1N1MucI7gG1JQcKS/poJKfvWjg==", endSmsResultjson);
                    //  log.LogInformation($"the data is  saved to jsondoc format , Message:: {Smspayload}");


                }
            }

           // Console.WriteLine(temp);
             Console.WriteLine(_prevmoisture);
           // Console.WriteLine(precipitation);
           // Console.WriteLine(_moisture);




        }
    }
}
