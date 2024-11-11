#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>

// Configuración del WiFi
const char* ssid = "FCQUNC";
const char* password = "fcquncquimica";

// URL de la nube donde subirás la información (por ejemplo, ThingSpeak o Firebase)
const char* serverName = "url_cloud";

void setup() {
  Serial.begin(115200);
  
  // Conexión al WiFi
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.println("Conectando al WiFi...");
  }
  Serial.println("Conexión establecida.");
}

void loop() {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;

    // Envío de datos
    int sensorData = analogRead(A0);  // Lee datos de un sensor
    String serverPath = serverName + String(sensorData);

    http.begin(serverPath.c_str());
    int httpResponseCode = http.GET();

    if (httpResponseCode > 0) {
      String response = http.getString();
      Serial.println(httpResponseCode);
      Serial.println(response);
    }
    else {
      Serial.print("Error en la conexión: ");
      Serial.println(httpResponseCode);
    }
    http.end();
  }
  delay(10000); // Espera 10 segundos entre envíos de datos
}
