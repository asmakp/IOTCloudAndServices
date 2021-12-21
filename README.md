# Moisture Monitoring System
##  Project description
The project aims at helping gardeners maintain their plants in good health by measuring the water content in the soil.
When the moisture levels are low or when the plants are not supplied with required amount of water, the soil moisture measuring 
system will reminder as SMS to the gardener to water the plants. Along with the moisture measurement the gardener is also provided with the weather
update about possibilities of rain or drizzle.

 ##  Target Group:
People with plants in their balconies and gardens, who often forget to water them.

##  Effect and benefit:
By maintaining the level of moisture in the soil we avoid plants from dying, the plants remain healthy who in turn contribute to healthy eco system.
By providing the rain update we not only save the consumption of water but also save plants from being destroyed because of over watering.

## 	Hardware
•	Capacitive Soil Moisture Sensor v2.0:
  It is made of corrosion-resistance material which gives long service life compared to the other resistive sensor avilable.
•	Esp32 -Wroom -EXPRESSIF: 
  I selected the ESP 32 as microcontroller, because of its multiple 12bits ADC inputs and the capability to generate PWM signals at higher frequencies 
## Software
• IDE and Languages:
  Arduino IDE is used to write the code in C language to drive the MCU along with sensor.

• Visual studio 19 IDE is used to write the code in C# for AZURE functions.
 	Along with .NET Core Framework which helps in building application and many .NET Core library which is provided with  NuGet package.
### 	Packages and Libraries used.
  Microsoft.Azure.WebJobs.Extention.CosmosDb
  Microsoft.Azure.WebJobs.Extention.EventsHubs
  Microsoft.Azure.WebJobs.Extention.Storage
  Microsoft.NET.Sdk.Functions
  SmhiWeather
  Twilio
## 	Cloud Service:
   ### Azure:
    Using Azure as Paas [ platform as a service]-
    Microsoft Azure can provide everything required to build an IoT solution without the need to invest
    in costly and fault-prone infrastructure that’s difficult to scale.
#### Azure IoT Hub:

    IOT Hub is the Cloud Gateway that stands between devices and the backend process.
    It acts as the interface point for the connected ‘things’ and the cloud.

####    MQTT:
   The Esp32 MCU (the device) communicates with IOH hub using MQTT protocol using libraries in the Azure IOT SDKs.
   Why MQTT?
   1.	Uses less battery power to publish and subscribe data.
   2.	Connection is secured using TLS /SSL where traffic is encrypted on the Web.

#### Azure Functions:
  Azure functions is a piece of code that is written to do a specific tasks, hence it is lightweight by design.
  Azure functions are serverless and do not require any Web servers or virtual machines to deploy and run. 
  Azure functions’ execution is triggered when an event is fired. Azure functions setup provides dozens of triggers that can be
  configured when an azure functions is execut-ed. 
  In this project one of the Azure function’s tasks is to Collect and process data received by the IOT hub.
  The function is triggered when a new data arrives at the IOT Hub. The processed data is sent to various database with the help of other azure functions.
  
#### Storage:
   Azure Cosmos DB[for warm storage] is a fully managed NoSQL database for modern app development.
  1. Schemas are not required to be defined at design time unlike relational     database. 
  2. No need to maintaining  complex parent-child entity relationships as in a  relational database.
  3. Easy to create and maintain.
  4. Since no schema design or table creation is required any changes in the structure of data can easily be enforced across databases.
  In this project the data is stored only when a new moisture reading is sent.
  BLOB STORAGE:
  Used as cold storage database.Azure Blob storage is Microsoft's object storage solution for the cloud.
  Blob storage is optimized for storing massive amounts of unstructured data. Unstructured data is data that doesn't adhere to 
  a particular data model or definition, such as text or binary data.
  In this project all the data sent by the dives is stored in Blob storage.
  
#### 	Twilio:

    Twilio is an American cloud communication platform as a service (CPaas) company based in California.  
    Twilio allows software developers to programmatically make and receive phone calls, send and receive text messages,
    and perform other communication functions using its web services API.
    In this project our client receives sms when the moisture content is low.
    
#### 	External API:
     SMHI Open Data API:
     An open API which provides weather data in a Json document format.
     Temperature, humidity and precipitation data is collected.
     Precipitaion data predicts the results of rain or drizzle.
     
    
 ##  	Implementation
    •	Architecture design
  
   
    ![ArchitectureDesign](https://github.com/asmakp/IOTCloudAndServices/blob/2bae0055813b4b59184b6ff74d9acd32e4e82185/ArchitectureDesign.jpg)
     



  





    
