using System.Diagnostics;
//Request library
using System.Net;

using System.Text.Json;
using ClothesWeather;




string pubIp =  new System.Net.WebClient().DownloadString("https://api.ipify.org");
string? location = Get($"http://ip-api.com/json/{pubIp}");

GetIpClass? deserializedLocation = JsonSerializer.Deserialize<GetIpClass>(location);


// Using Api service for getting weather from your location (https://openweathermap.org/)

string apiKey = "Paste your Api key here";

var client = new HttpClient();
Debug.Assert(deserializedLocation != null, nameof(deserializedLocation) + " != null");
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={deserializedLocation.lat}&lon={deserializedLocation.lon}&appid={apiKey}"),
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();

    WeatherMapApiClass.Root myDeserializedClass = JsonSerializer.Deserialize<WeatherMapApiClass.Root>(body);
    
    //Console.Write(body);
    Console.WriteLine("Инфа на сегодня");
    Console.WriteLine("Город: " + myDeserializedClass.name);
    Console.WriteLine("Температура: " + Math.Round(myDeserializedClass.main.feels_like - 273, 1));
    Console.WriteLine("Скорость ветра: " + myDeserializedClass.wind.speed);
    //Console.WriteLine(Math.Round(myDeserializedClass.main.feels_like - 274,1));
}





// Get method for finding your Location
string Get(string uri)
{
    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
    webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

    using(HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
    using(Stream stream = response.GetResponseStream())
    using(StreamReader reader = new StreamReader(stream))
    {
        return reader.ReadToEnd();
    }
}