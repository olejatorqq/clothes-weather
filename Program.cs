using System.Diagnostics;
//Request library
using System.Net;
using ClothesWeather;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


//you can change these parameters according to your feeling

//summer - (19-40)
//bit_cold - (15-18)
//demi_season - (5-14)
//winter - (-30 - 4)

int summer_temperature = 18;
int bit_cold_temperature = 14;
int demi_season_temperatupe = 8;

string city;
double temperature;
double windSpeed;
bool rain = false;

string pubIp =  new System.Net.WebClient().DownloadString("https://api.ipify.org");
string location = Get($"http://ip-api.com/json/{pubIp}");

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
    
    WeatherMapApiClass.Root myDeserializedWeatherClass = JsonSerializer.Deserialize<WeatherMapApiClass.Root>(body);

    foreach (var weather in myDeserializedWeatherClass.weather)
    {
        if (weather.main == "Rain")
        {
            rain = true;
        }
    }
    
    city = myDeserializedWeatherClass.name;
    temperature = Math.Round(myDeserializedWeatherClass.main.feels_like - 273, 1);
    windSpeed = myDeserializedWeatherClass.wind.speed;
}


Console.WriteLine("\nВаш Город: " + city);
Console.WriteLine("\nПрогноз на сегодня");
Console.WriteLine("Температура: " + temperature);
Console.WriteLine("Скорость ветра: " + windSpeed);


/*string jsonString =
    @"{""Clothes"": [{""must_have"": {""body"": ""Футболка""}, ""summer"": {""shorts"" : ""Шорты""}, ""bit_cold"" : {""shorts"": ""Штаны/Джинсы""}, ""demi-season"": {""upper_body"": ""Ветровка"", ""shorts"" : ""Штаны/Джинсы""}, ""winter"": {""upper_body"" : ""Куртка"", ""body"" : ""Кофта"", ""shorts"" : ""Штаны/Джинсы""}}]}";
ClothesJsonDeserializeClass clothesJsonDeserializeClass = JsonSerializer.Deserialize<ClothesJsonDeserializeClass>(jsonString);*/

dynamic jsonfile = JsonConvert.DeserializeObject("{'Clothes': [{'summer': {'upper_body': '', 'body': 'Футболка', 'shorts' : 'Шорты'}, 'bit_cold' : {'upper_body': '', 'body': 'Футболка', 'shorts': 'Штаны/Джинсы'}, 'demi_season': {'upper_body': 'Ветровка', 'body' : 'Кофта', 'shorts' : 'Штаны/Джинсы'}, 'winter': {'upper_body' : 'Куртка', 'body' : 'Кофта', 'shorts' : 'Штаны/Джинсы'}}]}");


/*foreach (var clotherDeserialize in jsonfile.Clothes)
{
    if (temperature > summer_temperature)//18
    {
        Console.WriteLine($"Вам следует надеть: {clotherDeserialize.summer.upper_body} {clotherDeserialize.summer.body} и {clotherDeserialize.summer.shorts}");
    }
    else if (temperature > bit_cold_temperature)//14
    {
        Console.WriteLine($"Вам следует надеть: {clotherDeserialize.bit_cold.upper_body} {clotherDeserialize.bit_cold.body} и {clotherDeserialize.bit_cold.shorts}");
    }
    else if (temperature > demi_season_temperatupe)
    {
        Console.WriteLine($"Вам следует надеть: {clotherDeserialize.bit_cold.upper_body} {clotherDeserialize.bit_cold.body} и {clotherDeserialize.bit_cold.shorts}");
    }
}*/

if (rain)
{
    Console.Write("Сейчас идет дождь, возьмите зонт");
}




/*foreach (var qq in stuff.Clothes)
{
    Console.Write(qq.must_have.body);
}*/









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