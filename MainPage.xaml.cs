using WeatherWiki.DataProvider;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherWiki
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            APIHelper.InitializeClient();
        }

        private async void getWeather(string userInput)
        {
            var wdp = new WeatherDataProvider();
            var weather = await wdp.GetWeather(userInput);

            if (weather == null)
            {
                error_message.Text = "Invalid city name";
                return;
            }

            else
            {
                error_message.Text = " ";
            }

            CurrentWeatherComponent.AddCurrentWeatherDataToUI(weather);
            ForecastWeatherComponent.AddForecastWeatherDataToUI(weather);
        }

        private void Search_City(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                getWeather(user_input.Text);
            }
        }
    }
}
