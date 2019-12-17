using System;
using System.Collections.ObjectModel;
using WeatherWiki.Models;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

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
        public CurrentWeather()
        {
            this.InitializeComponent();
        }

        public void AddCurrentWeatherDataToUI(WeatherRoot weather)
        {
            var processedObject = ProcessObject(weather);

            ObservableCollection<CurrentWeatherData> currentWeather = new ObservableCollection<CurrentWeatherData>
            {
                processedObject
            };

            observableColletionCurrentWeather.ItemsSource = currentWeather;
        }

        private CurrentWeatherData ProcessObject(WeatherRoot weather)
        {
            return new CurrentWeatherData
            {
                City = weather.City,
                ImagePath = $"/Images/WeatherState/{weather.WeatherData[0].Weather.WeatherIcon}.png",
                Temperature = weather.WeatherData[0].CurrentTemperature,
                Condition = weather.WeatherData[0].Weather.ConditionDescription,
                Updated = DateTime.Now.ToString("HH:mm")
            };
        }
    }
}
