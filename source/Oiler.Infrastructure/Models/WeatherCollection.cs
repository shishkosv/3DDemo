
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Oiler.Infrastructure.Models
{
    public class WeatherCollection : ObservableCollection<WeatherHistoryItem>
    {
        public WeatherCollection()
        {
        }

        public WeatherCollection(IEnumerable<WeatherHistoryItem> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            foreach (WeatherHistoryItem marketHistoryItem in list)
            {
                this.Add(marketHistoryItem);
            }
        }
    }

    public class WeatherHistoryItem
    {
        public DateTime DateTimeMarker { get; set; }

        public int Value { get; set; }
    }
}