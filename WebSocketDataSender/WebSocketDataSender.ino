#include <WiFi.h>
#include <WebSocketClient.h>
#include "DHT.h"

#define DHTTYPE DHT22
#define WIFI_SSID "NETIASPOT-2,4GHz-69A020"
#define WIFI_PASS "yygjMZJbv46r"

#define HOST "192.168.1.100"
#define PORT 7890
#define PATH "/Reading"

WebSocketClient webSocketClient;
WiFiClient client;
uint8_t DHTPin = 4; 
float Temperature;
float Humidity;
               
DHT dht(DHTPin, DHTTYPE);                

void setup() {
  Serial.begin(115200);
  delay(100);

  pinMode(DHTPin, INPUT);
  dht.begin();

  if(connect() == 0) { return ; }
  if(initWebSocket() == 0) { return ; }
}

void loop() {
  if (client.connected()){
    String tTemp, tHum;
    tTemp = readDHTTemperature();
    tHum =  readDHTHumidity();
    webSocketClient.sendData(tTemp + ";" + tHum);
    
    Serial.println("Sending new data: " + tTemp + ";" + tHum);
    delay(10000);
  }
  else {
    Serial.print("Disconnected from server. Connecting again...");
    if(connect() == 0) { return ; }
    if(initWebSocket() == 0) { return ; }
  }
}

String readDHTTemperature() {
  float t = dht.readTemperature();
  if (isnan(t)) {    
    return "--";
  }
  else {
    return String(t);
  }
}

String readDHTHumidity() {
  float h = dht.readHumidity();
  if (isnan(h)) {
    return "--";
  }
  else {
    return String(h);
  }
}

char connect()
{
  WiFi.begin(WIFI_SSID, WIFI_PASS); 

  Serial.println("Waiting for wifi");
  int timeout_s = 30;
  while (WiFi.status() != WL_CONNECTED && timeout_s-- > 0) {
      delay(1000);
      Serial.print(".");
  }
  
  if(WiFi.status() != WL_CONNECTED)
  {
    Serial.println("unable to connect, check your credentials");
    return 0;
  }
  else
  {
    Serial.println("Connected to the WiFi network");
    Serial.println(WiFi.localIP());
    return 1;
  }
}

char initWebSocket()
{
  if(!client.connect(HOST, PORT)) {
    Serial.println("Connection failed to WebSocket Server.");
    return 0;
  }

  Serial.println("Connected to WebSocket Server.");
  webSocketClient.path = PATH;
  webSocketClient.host = "192.168.1.100:7890";

  if (!webSocketClient.handshake(client)) {
    Serial.println("Handshake failed.");
    return 0;
  }
  Serial.println("Handshake successful");
  return 1;
}
