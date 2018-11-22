
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using Oiler.Infrastructure.Interfaces;
//using Oiler.Infrastructure.Models;

//namespace Oiler.Infrastructure.Services
//{
//    public class WeatherService : IWeatherService
//    {
//        private Dictionary<string, WeatherCollection> _history;

//        private static string url =
//            "http://api.openweathermap.org/data/2.5/forecast";
//        private string urlParameters = "?id=524901&APPID=b39d0c256b3f32f2f98c6e1f78399c15";

//        public WeatherService()
//        {
//            InitializeMarketHistory();
//        }

//        private void InitializeMarketHistory()
//        {
//            _history = new Dictionary<string, WeatherCollection>();

//            HttpClient client = new HttpClient();
//            client.BaseAddress = new Uri(url);

//            // Add an Accept header for JSON format.
//            client.DefaultRequestHeaders.Accept.Add(
//                new MediaTypeWithQualityHeaderValue("application/json"));

//            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
//            if (response.IsSuccessStatusCode)
//            {
//                var dataObjects = response.Content.<IEnumerable<WeatherHistoryItem>>();
//                foreach (var d in dataObjects)
//                {
//                    Console.WriteLine("{0}", d.Name);
//                }
//            }
//            else
//            {
//                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
//            }
//        }

//        public WeatherCollection GetWeatherHistory(string citySymbol)
//        {
//            WeatherCollection items = _history[citySymbol];
//            return items;
//        }
//    }
//}
