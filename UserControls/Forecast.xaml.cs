using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherWiki.Models;
using Windows.UI.Xaml.Controls;

namespace WeatherWiki.UserControls
{
    public class ForecastDay
    {
        public string Day { get; set; }
        public string ImagePath { get; set; }
        public double HighTemperature { get; set; }
        public double LowTemperature { get; set; }
        public string Condition { get; set; }
    }

    public sealed partial class Forecast : UserControl
    {
        private ObservableCollection<ForecastDay> forecast;

        public Forecast()
        {
            this.InitializeComponent();
            forecast = new ObservableCollection<ForecastDay>();
        }

        public void AddForecastWeatherDataToUI(List<WeatherData> weatherData)
        {
            forecast.Clear();

            foreach (var data in weatherData.Skip(1)) // Skip(1) because first object is not an forecast object.
            {
                forecast.Add(ProcessObject(data));
            }

            forecastItemsControl.ItemsSource = forecast;
        }

        private ForecastDay ProcessObject(WeatherData weatherData)
        {
            return new ForecastDay()
            {
                Day = ProcessDate(weatherData.datetime),
                ImagePath = ProcessImagePath(weatherData.Weather.WeatherIcon),
                HighTemperature = weatherData.HighTemperature,
                LowTemperature = weatherData.LowTemperature,
                Condition = weatherData.Weather.ConditionDescription
            };
        }

        private string ProcessImagePath(string weatherIcon)
        {
            return $"/Images/WeatherState/{weatherIcon}.png";
        }

        private string ProcessDate(string date)
        {
            DateTime dateTime = DateTime.Parse(date);
            date = dateTime.ToString("ddd") + " " + dateTime.Day.ToString();

            return date;
        }
    }
}
