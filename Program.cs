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
using static ClothesWeather.GetIpClass;


string pubIp =  new System.Net.WebClient().DownloadString("https://api.ipify.org");
string? location = Get($"http://ip-api.com/json/{pubIp}");

GetIpClass asd = JsonSerializer.Deserialize<GetIpClass>(location);

Console.Write(asd.lat + " " + asd.lon);



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