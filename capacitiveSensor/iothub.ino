void initIotDevice(){
  
 char* conn = "HostName=iot20-iotasmhub.azure-devices.net;DeviceId=esp32;SharedAccessKey=FhKzWL2G0wv+HV2tecMQg9+2fYUczp3h1nOGvDqa9lg=";

 bool isConnectedToIotHub = false;
 
  if(!Esp32MQTTClient_Init((const uint8_t * ) conn)){
    isConnectedToIotHub = false;
    return;
  }
  isConnectedToIotHub = true;
}

/*void sendMessage(char *payload) {
  
    
  EVENT_INSTANCE *message = Esp32MQTTClient_Event_Generate(payload, MESSAGE);
    
    Esp32MQTTClient_Event_AddProp(message, "SchoolName", "Nackademin");
    Esp32MQTTClient_Event_AddProp(message, "StudentName", "Asma");
  
  if( Esp32MQTTClient_SendEventInstance(message))
   { Serial.println((char *)message);}
  
}*/
