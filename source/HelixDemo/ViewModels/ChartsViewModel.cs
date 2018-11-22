
using System.Collections.ObjectModel;
using System.Linq;
using Oiler.Infrastructure.Constants;
using Oiler.Infrastructure.Interfaces;
using Oiler.Infrastructure.Models;
using Oiler.Infrastructure.Services;
using Prism.Mvvm;

namespace Oiler.ViewModels
{
    public class ChartsViewModel : BindableBase
    {
        private readonly IWeatherService _weatherService;

        private string _currentCity;

        private WeatherCollection _weatherCollection;

        private GroupWeatherCollection _groupWeatherCollection;

        private ObservableCollection<string> _cities;

        public ChartsViewModel()
        {
            _weatherService = new MockWeatherService();
            CurrentCity = Constants.Cities.FirstOrDefault();
            HistoryCollection = _weatherService.GetWeatherHistory(CurrentCity);
            GroupHistoryCollection = _weatherService.GetWeatherBreakdown(CurrentCity);
        }

        public void TickerSymbolChanged(string newCity)
        {
            this.CurrentCity = newCity;
            WeatherCollection newHistoryCollection = _weatherService.GetWeatherHistory(newCity);
            HistoryCollection = newHistoryCollection;

            GroupWeatherCollection newGroupCollection = _weatherService.GetWeatherBreakdown(newCity);
            GroupHistoryCollection = newGroupCollection;
        }

        public string CurrentCity
        {
            get
            {
                return _currentCity;
            }
            set
            {
                SetProperty(ref _currentCity, value);
            }
        }

        public WeatherCollection HistoryCollection
        {
            get
            {
                return _weatherCollection;
            }
            private set
            {
                SetProperty(ref this._weatherCollection, value);
            }
        }

        public GroupWeatherCollection GroupHistoryCollection
        {
            get
            {
                return _groupWeatherCollection;
            }
            private set
            {
                SetProperty(ref _groupWeatherCollection, value);
            }
        }

        public ObservableCollection<string> CitiesCollection
        {
            get
            {
                return new ObservableCollection<string>(Constants.Cities);
            }
            private set
            {
                SetProperty(ref _cities, value);
            }
        }
    }
}
