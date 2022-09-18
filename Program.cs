// See https://aka.ms/new-console-template for more information
using System.Device.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Request library
using System.Net;
using System.IO;
using System.Text.Json;
using ClothesWeather;



string pubIp =  new System.Net.WebClient().DownloadString("https://api.ipify.org");
string? location = Get($"http://ip-api.com/json/{pubIp}");

GetIpClass locationInfo = JsonSerializer.Deserialize<GetIpClass>(location);

//Console.Write(locationInfo.lat + " " + locationInfo.lon);


// Using Api service for getting weather from your location (https://openweathermap.org/)

string apiKey = "Paste your Api key here";

var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={locationInfo.lat}&lon={locationInfo.lon}&appid={apiKey}"),
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}





// Get method for find your Location
string Get(string uri)
{
    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

    using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
    using(Stream stream = response.GetResponseStream())
    using(StreamReader reader = new StreamReader(stream))
    {
        return reader.ReadToEnd();
    }
}