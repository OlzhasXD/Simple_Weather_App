using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter City Name: ");
            string name = Console.ReadLine();
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + name +
                         "&units=metric&appid=6cfdadb6edf082ceec91f1f44f263962";

            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);


                string data = sr.ReadToEnd();

                WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(data);

                Console.Write("City name: {0} \nPressure: {1} \nTemp: {2} C", weatherInfo.Name,
                    weatherInfo.Main.Pressure, weatherInfo.Main.Temp);
                response.Close();
            }
            catch (WebException e)
            {
                Console.Write("Exception Occured. Check the correctness of city name");
            }
        }
    }

    class TempInfo
    {
        public double Temp { get; set; }
        public double Pressure { get; set; }
    }

    class WeatherInfo
    {
        public TempInfo Main { get; set; }
        public string Name { get; set; }
    }
}