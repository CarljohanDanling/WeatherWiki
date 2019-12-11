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
    public sealed partial class Forecast : UserControl
    {
        public Forecast()
        {
            this.InitializeComponent();

            ObservableCollection<ForecastDay> forecastDays = new ObservableCollection<ForecastDay>
            {
                new ForecastDay()
                {
                    Day = "Wed 28",
                    ImagePath = "/Images/WeatherState/a01d.png",
                    Temperature = "1˚, -5˚",
                    Condition = "Windy"
                },
                new ForecastDay()
                {
                    Day = "Tue 12",
                    ImagePath = "/Images/WeatherState/c04d.png",
                    Temperature = "1˚, -5˚",
                    Condition = "Rain"
                },
                new ForecastDay()
                {
                    Day = "Sun 3",
                    ImagePath = "/Images/WeatherState/d01d.png",
                    Temperature = "1˚, -5˚",
                    Condition = "Clear"
                },
                new ForecastDay()
                {
                    Day = "Fri 22",
                    ImagePath = "/Images/WeatherState/f01d.png",
                    Temperature = "1˚, -5˚",
                    Condition = "Windy"
                },
                new ForecastDay()
                {
                    Day = "Thu 4",
                    ImagePath = "/Images/WeatherState/s03d.png",
                    Temperature = "1˚, -5˚",
                    Condition = "Rain"
                },
                                new ForecastDay()
                {
                    Day = "Wed 7",
                    ImagePath = "/Images/WeatherState/s01d.png",
                    Temperature = "1˚, -5˚",
                    Condition = "Rain"
                },
                                                new ForecastDay()
                {
                    Day = "Sat 29",
                    ImagePath = "/Images/WeatherState/t02n.png",
                    Temperature = "1˚, -5˚",
                    Condition = "Rain"
                }
            };

            observableColletionForecast.ItemsSource = forecastDays;
        }
    }

    public class ForecastDay
    {
        public string Day { get; set; }
        public string ImagePath { get; set; }
        public string Temperature { get; set; }
        public string Condition { get; set; }
    }
}
