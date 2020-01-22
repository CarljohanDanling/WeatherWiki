using System;
using System.Collections.ObjectModel;
using WeatherWiki.Models;
using Windows.UI.Xaml.Controls;

namespace WeatherWiki.UserControls
{
    public class CurrentWeatherData
    {
        public string City { get; set; }
        public string ImagePath { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; }
        public string Updated { get; set; }
    }

    public sealed partial class CurrentWeather : UserControl
    {
        private ObservableCollection<CurrentWeatherData> currentWeather;

        public CurrentWeather()
        {
            this.InitializeComponent();
            currentWeather = new ObservableCollection<CurrentWeatherData>();
        }

        public void AddCurrentWeatherDataToUI(WeatherRoot weather)
        {
            currentWeather.Clear();
            currentWeather.Add(ProcessObject(weather));
            currentWeatherItemsControl.ItemsSource = currentWeather;
        }

        private CurrentWeatherData ProcessObject(WeatherRoot weather)
        {
            return new CurrentWeatherData
            {
                City = weather.City,
                ImagePath = ProcessImagePath(weather.WeatherData[0].Weather.WeatherIcon),
                Temperature = weather.WeatherData[0].CurrentTemperature,
                Condition = weather.WeatherData[0].Weather.ConditionDescription,
                Updated = DateTime.Now.ToString("HH:mm")
            };
        }

        private string ProcessImagePath(string weatherIcon) 
            => $"/Images/WeatherState/{weatherIcon}.png";
    }
}
