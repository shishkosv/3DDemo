
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Oiler.Infrastructure.Models
{
    public class GroupWeatherCollection : ObservableCollection<WeatherGroupItem>
    {
        public GroupWeatherCollection()
        {
        }

        public GroupWeatherCollection(IEnumerable<WeatherGroupItem> list)
        {
            if (list == null) throw new ArgumentNullException("list");

            foreach (WeatherGroupItem item in list)
            {
                Add(item);
            }
        }
    }

    public class WeatherGroupItem
    {
        public int Counts { get; set; }

        public int Temperature { get; set; }
    }
}