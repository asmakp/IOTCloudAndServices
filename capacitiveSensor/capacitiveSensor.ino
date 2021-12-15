#include <WiFi.h>
#include <Esp32MQTTClient.h>
#include "time.h"
#include <ArduinoJson.h>


char* ssid = "";
char* pass = "";

//To impliment delay
const int interval = 10000;
unsigned long prevMilles = 0;

//values from the weather data
float temperature = -4.0;
float humidity = 30.00;
int precipitation = 30;

//To measure moisture sensor value
int percentageMoisture;

const int dry = 3495;
const int wet = 1302;

//To serialalize the json doc
char payload[256];

//To store time data
char Timebuff[35];

void setup() {

  Serial.begin(9600);
  initWifi();
  initIotDevice();

  char* ntpServer = "pool.ntp.org";
  const long  gmtOffset_sec = 3600;
  const int   daylightOffset_sec = 3600;
 
  //init and get the time
  configTime(gmtOffset_sec, daylightOffset_sec, ntpServer);
  printLocalTime();

}

void loop() {
   printLocalTime();
  unsigned long currentMillis = millis();
  if (currentMillis - prevMilles  >= interval) {
    prevMilles  = currentMillis;
    int sensorVal = analogRead(A9);
    // Serial.println(sensorVal);
    percentageMoisture = map(sensorVal , wet, dry , 100, 0);

    Serial.print(percentageMoisture);
    Serial.println("%");

    StaticJsonDocument<50> doc;                         //Creating json document
    doc["PercentMoisture"] = percentageMoisture;
    doc["MsgCreateTime"] = Timebuff;

    serializeJson(doc, payload);
   // Serial.println(payload);

    if (Esp32MQTTClient_SendEvent((char *)payload)) {  //Sending the serialized json document to IOT Hub
      Serial.println((char *) payload);
    }

    //sendMessage(payload);  another funtion if sending properties
  }
}
