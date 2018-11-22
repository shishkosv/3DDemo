
using System;
using System.Collections.Generic;
using System.Linq;
using Oiler.Infrastructure.Interfaces;
using Oiler.Infrastructure.Models;
using Oiler.Infrastructure.Utilities;

namespace Oiler.Infrastructure.Services
{
    public class MockWeatherService : IWeatherService
    {
        private Dictionary<string, WeatherCollection> _history;

        private Dictionary<string, GroupWeatherCollection> _groupedHistory;

        public MockWeatherService()
        {
            InitializeMarketHistory();
        }

        private void InitializeMarketHistory()
        {
            _history = new Dictionary<string, WeatherCollection>();
            _groupedHistory = new Dictionary<string, GroupWeatherCollection>();
            foreach (string city in Constants.Constants.Cities)

            {
                var cityHistory = GetCurrentCityHistory();
                _history.Add(city, cityHistory);
                _groupedHistory.Add(city, GetCurrentGroupHistory(cityHistory));
            }
        }

        private WeatherCollection GetCurrentCityHistory()
        {
            var startDate = DateTime.Now.AddDays(-DaysAgoQty);
            var result = new WeatherCollection();
            for (var i = 0; i < DaysAgoQty * 24; i++)
            {
                var item = new WeatherHistoryItem
                {
                    DateTimeMarker = startDate.AddHours(i),
                    Value = MathUtilities.GetRandomNumber(65, 95)
                };
                result.Add(item);
            }

            return result;
        }

        private GroupWeatherCollection GetCurrentGroupHistory(WeatherCollection history)
        {
            var result = new GroupWeatherCollection();
            var tempQuery =
                from hour in history
                group hour by hour.Value into newGroup
                orderby newGroup.Key
                select newGroup;

            foreach (var g in tempQuery)
            {
                result.Add(new WeatherGroupItem {                               
                    Counts = g.Count(),
                    Temperature = g.Key
                });
            }

            return result;
        }

        public const int DaysAgoQty = 10;


        public WeatherCollection GetWeatherHistory(string citySymbol)
        {
            WeatherCollection items = _history[citySymbol];
            return items;
        }

        public GroupWeatherCollection GetWeatherBreakdown(string citySymbol)
        {
            GroupWeatherCollection items = _groupedHistory[citySymbol];
            return items;
        }
    }
}
