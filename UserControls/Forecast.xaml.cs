using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void AddForecastWeatherDataToUI(WeatherRoot weather)
        {
            forecast.Clear();

            var listOfForecastWeather = ProcessObject(weather);
            listOfForecastWeather.ForEach(x => forecast.Add(x));

            observableColletionForecast.ItemsSource = forecast;
        }

        public List<ForecastDay> ProcessObject(WeatherRoot weather)
        {   
            // First object is not a forecast object.
            weather.WeatherData.RemoveAt(0);

            List<ForecastDay> forecastDays = new List<ForecastDay>();

            foreach (var item in weather.WeatherData)
            {
                DateTime dateTime = DateTime.Parse(item.Date);
                item.Date = dateTime.ToString("ddd") + " " + dateTime.Day.ToString();

                var forecastDay = new ForecastDay
                {
                    Day = item.Date,
                    ImagePath = $"/Images/WeatherState/{item.Weather.WeatherIcon}.png",
                    HighTemperature = item.HighTemperature,
                    LowTemperature = item.LowTemperature,
                    Condition = item.Weather.ConditionDescription
                };

                forecastDays.Add(forecastDay);
            }

            return forecastDays;
        }
    }
}
