using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IOTproject
{
    public static class ToCosmosDB
    {
        [FunctionName("ToCosmosDB")]
        public static void Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
             [CosmosDB(
            databaseName:"cosmosdb",      //the database under cosmos on azure cloud
            collectionName: "Messages1",
            ConnectionStringSetting = "CosmosDbConnection", //connection string to the cosmos DB
            CreateIfNotExists =true
            )]out dynamic cosmos,
            ILogger log)
        {
           
           
            string data = new StreamReader(req.Body).ReadToEnd();
          
            log.LogInformation($" data sent to cosmos function,:: {data }");

            try
            {
                cosmos = data;
               
            }
            catch
            {
                cosmos = null;
              
            }


        }
    }
}
