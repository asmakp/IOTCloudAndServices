using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text;
using IOTproject.Model;

namespace IOTproject
{
    public static class ToBlobStorage
    {
        [FunctionName("ToBlobStorage")]
        public static async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody =  await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<dataHandle>(requestBody);

            log.LogInformation($" data sent to Blob function,:: {data }");

            string filename;
            filename = Guid.NewGuid().ToString("n");

            await CreateBlob(filename + ".json", data, log);



            //  return new OkResult();
        }
        private static async Task CreateBlob(string id, dataHandle data, ILogger log)
        {
            string connectionString;
            CloudStorageAccount storageAccount;
            CloudBlobClient client;
            CloudBlobContainer container;
            CloudBlockBlob blob;


            //connectionstring for function app - access keys
            connectionString = "DefaultEndpointsProtocol=;BlobEndpoint=QueueEndpoint=FileEndpoint=";
            storageAccount = CloudStorageAccount.Parse(connectionString);
            client = storageAccount.CreateCloudBlobClient();
            container = client.GetContainerReference("blobmessages");

            await container.CreateIfNotExistsAsync();

            blob = container.GetBlockBlobReference(id);
            blob.Properties.ContentType = "application/json";

            // await blob.UploadFromStreamAsync(new MemoryStream(data));
            // await blob.UploadTextAsync(data);
            // await blob.UploadFromStreamAsync(new MemoryStream(Encoding.UTF8.GetBytes(data))); //since data is already in the form of json
            // await blob.UploadTextAsync(new MemoryStream(Encoding.UTF8.GetBytes(data));
           // await blob.UploadFromStreamAsync(new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)))); //since data is already in the form of json
            await blob.UploadTextAsync(JsonConvert.SerializeObject(data));
        }
    }


}
