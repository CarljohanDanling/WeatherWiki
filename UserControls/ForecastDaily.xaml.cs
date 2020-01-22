using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherWiki.Models;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace WeatherWiki.UserControls
{
    public class ForecastDailyData
    {
        public string Day { get; set; }
        public string ImagePath { get; set; }
        public double HighTemperature { get; set; }
        public double LowTemperature { get; set; }
        public string Condition { get; set; }
    }

    public sealed partial class ForecastDaily : UserControl
    {
        private ObservableCollection<ForecastDailyData> forecastDaily;

        private event TappedEventHandler _individualDayTapped;
        public event TappedEventHandler IndividualDayTapped
        {
            add { _individualDayTapped += value; }
            remove { }
        }

        public ForecastDaily()
        {
            this.InitializeComponent();
            forecastDaily = new ObservableCollection<ForecastDailyData>();
        }

        public void AddDailyForecastWeatherDataToUI(List<WeatherData> weatherData)
        {
            forecastDaily.Clear();

            foreach (var data in weatherData.Skip(1)) // Skip(1) because first object is not an forecast object.
            {
                forecastDaily.Add(ProcessObject(data));
            }

            forecastDailyItemsControl.ItemsSource = forecastDaily;
        }

        private ForecastDailyData ProcessObject(WeatherData weatherData)
        {
            return new ForecastDailyData()
            {
                Day = ProcessDate(weatherData.datetime),
                ImagePath = ProcessImagePath(weatherData.Weather.WeatherIcon),
                HighTemperature = weatherData.HighTemperature,
                LowTemperature = weatherData.LowTemperature,
                Condition = weatherData.Weather.ConditionDescription
            };
        }

        private string ProcessImagePath(string weatherIcon)
            => $"/Images/WeatherState/{weatherIcon}.png";

        private string ProcessDate(string date)
        {
            DateTime dateTime = DateTime.Parse(date);
            date = dateTime.ToString("ddd") + " " + dateTime.Day.ToString();

            return date;
        }

        private void PointerEnteredForecast(object sender, PointerRoutedEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            sp.Background = ColorConverter("black");
        }

        private void PointerExitedForecast(object sender, PointerRoutedEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            sp.Background = null;
        }

        private SolidColorBrush ColorConverter(string desiredColor)
        {
            var color = (Color)XamlBindingHelper.ConvertValue(typeof(Color), desiredColor);

            return new SolidColorBrush
            {
                Color = color,
                Opacity = 0.3
            };
        }

        private void IndividualDay_Tapped(object sender, TappedRoutedEventArgs e)
            => _individualDayTapped(sender, e);
    }
}