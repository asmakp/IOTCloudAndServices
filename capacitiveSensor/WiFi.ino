void initWifi(){
   WiFi.begin(ssid, pass);

  while(WiFi.status()!= WL_CONNECTED)
  {
    delay(1000);
    Serial.println(".");
  }
 Serial.print("\nIP Address: ");
 Serial.println(WiFi.localIP());
 
}
