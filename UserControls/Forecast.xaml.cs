using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeatherWiki.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

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
        public Forecast()
        {
            this.InitializeComponent();
        }

        public void AddForecastWeatherDataToUI(WeatherRoot weather)
        {
            var listOfForecastWeather = ProcessObject(weather);

            ObservableCollection<ForecastDay> forecastDays = new ObservableCollection<ForecastDay>();
            
            foreach (var forecast in listOfForecastWeather)
            {
                forecastDays.Add(forecast);
            }

            observableColletionForecast.ItemsSource = forecastDays;
        }

        public List<ForecastDay> ProcessObject(WeatherRoot weather)
        {
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
