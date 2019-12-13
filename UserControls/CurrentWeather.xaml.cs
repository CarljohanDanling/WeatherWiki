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
            string city = weather.City;
            string weatherIcon = weather.WeatherData[0].Weather.WeatherIcon;
            double currentTemperature = weather.WeatherData[0].CurrentTemperature;
            string condition = weather.WeatherData[0].Weather.ConditionDescription;
            string updated = DateTime.Now.ToString("HH:mm");

            ObservableCollection<CurrentWeatherData> currentWeather = new ObservableCollection<CurrentWeatherData>
            {
                new CurrentWeatherData()
                {
                    City = city,
                    ImagePath = $"/Images/WeatherState/{weatherIcon}.png",
                    Temperature = currentTemperature,
                    Condition = condition,
                    Updated = "Updated as of " + updated
                }
            };

            observableColletionCurrentWeather.ItemsSource = currentWeather;
        }
    }
}
