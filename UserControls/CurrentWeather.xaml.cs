using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class CurrentWeather : UserControl
    {
        public CurrentWeather()
        {
            this.InitializeComponent();

            ObservableCollection<CurrentWeatherData> currentWeather = new ObservableCollection<CurrentWeatherData>
            {
                new CurrentWeatherData()
                {
                    City = "Skövde",
                    ImagePath = "/Images/WeatherState/a01d.png",
                    Temperature = 3.6,
                    Condition = "Cloudy",
                    Updated = DateTime.Now.ToString("HH:mm:ss tt")
                }
            };

            observableColletionCurrentWeather.ItemsSource = currentWeather;
        }

        public class CurrentWeatherData
        {
            public string City { get; set; }
            public string ImagePath { get; set; }
            public double Temperature { get; set; }
            public string Condition { get; set; }
            public string Updated { get; set; }
        }
    }
}
