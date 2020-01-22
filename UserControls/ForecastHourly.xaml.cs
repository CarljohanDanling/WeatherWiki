using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherWiki.Models;
using Windows.UI.Xaml.Controls;

namespace WeatherWiki.UserControls
{
    public class ForecastHourlyData
    {
        public string Hour { get; set; }
        public string ImagePath { get; set; }
        public double Temperature { get; set; }
    }

    public sealed partial class ForecastHourly : UserControl
    {
        private ObservableCollection<ForecastHourlyData> forecastHourly;

        public ForecastHourly()
        {
            this.InitializeComponent();
            forecastHourly = new ObservableCollection<ForecastHourlyData>();
        }

        public void AddHourlyForecastWeatherDataToUI(WeatherRoot weather, string typeOfData)
        {
            forecastHourly.Clear();

            if (typeOfData == "randomized")
            {
                weather.WeatherData = RandomizeHourlyForecast(weather.WeatherData);
            }

            foreach (var item in FilterList(weather.WeatherData))
            {
                forecastHourly.Add(ProcessObject(item));
                forecastHourlyItemsControl.ItemsSource = forecastHourly;
            }
        }

        // API only gives 24 hours hourly forecast. I randomize the temperature data
        // when clicking on individual days for the sake of visual experience.
        public List<WeatherData> RandomizeHourlyForecast(List<WeatherData> listOfWeatherData)
        {
            var random = new Random();

            return listOfWeatherData.Select(w =>
            {
                w.CurrentTemperature = random.Next
                (
                    (int)w.CurrentTemperature - 3,
                    (int)w.CurrentTemperature + 3
                );
                return w;
            }).ToList();
        }

        private ForecastHourlyData ProcessObject(WeatherData weatherData)
        {
            return new ForecastHourlyData()
            {
                Hour = ProcessTime(weatherData.HourOfDay),
                ImagePath = ProcessImagePath(weatherData.Weather.WeatherIcon),
                Temperature = weatherData.CurrentTemperature
            };
        }

        private string ProcessImagePath(string weatherIcon)
        {
            return $"/Images/WeatherState/{weatherIcon}.png";
        }

        private string ProcessTime(string time)
        {
            return time.Substring(time.IndexOf("T") + 1, 5);
        }

        private IEnumerable<WeatherData> FilterList(List<WeatherData> weatherData)
        {
            return weatherData.Where((x, i) => i % 2 == 0).Skip(1);
        }
    }
}
