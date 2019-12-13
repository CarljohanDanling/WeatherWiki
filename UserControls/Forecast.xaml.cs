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
    public sealed partial class Forecast : UserControl
    {
        public Forecast()
        {
            this.InitializeComponent();

            ObservableCollection<ForecastDay> forecastDays = new ObservableCollection<ForecastDay>
            {

            };

            observableColletionForecast.ItemsSource = forecastDays;
        }
    }

    public class ForecastDay
    {
        public string Day { get; set; }
        public string ImagePath { get; set; }
        public double HighTemperature { get; set; }
        public double LowTemperature { get; set; }
        public string Condition { get; set; }
    }
}
