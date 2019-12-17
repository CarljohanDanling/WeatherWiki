using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherWiki.DataProvider;
using WeatherWiki.Models;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherWiki
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<String> suggestions;

        public MainPage()
        {
            this.InitializeComponent();
            APIHelper.InitializeClient();
            ApplicationView.PreferredLaunchViewSize = new Size(1120, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            suggestions = new ObservableCollection<string>();
        }

        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var listOfSuggestions = await getSuggestion(sender.Text);

                if (listOfSuggestions == null)
                {
                    return;
                }

                else
                {
                    suggestions.Clear();
                    listOfSuggestions.ForEach(x => suggestions.Add(x.SuggestedValue));
                    sender.ItemsSource = suggestions;
                }
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            // If user presses 'enter' instead of clicking, preventing null exception.
            if (args.QueryText != null && args.ChosenSuggestion == null)
            {
                getWeather(args.QueryText);
            }

            else
            {
                string autoSuggestBoxInput = args.ChosenSuggestion.ToString();
                // Removing comma sign from the string.
                getWeather(autoSuggestBoxInput.Substring(0, autoSuggestBoxInput.IndexOf(",")));
            }
        }

        private async Task<List<Suggestion>> getSuggestion(string userInput)
        {
            var sdp = new SuggestionDataProvider();
            var suggestion = await sdp.GetSuggestion(userInput);

            return suggestion.Suggestions;
        }

        private async void getWeather(string userInput)
        {
            var wdp = new WeatherDataProvider();
            var weather = await wdp.GetWeather(userInput);

            if (weather == null)
            {
                errorMessage.Text = "Invalid city name";
                return;
            }

            else
            {
                errorMessage.Text = " ";
            }

            CurrentWeatherComponent.AddCurrentWeatherDataToUI(weather);
            ForecastWeatherComponent.AddForecastWeatherDataToUI(weather);
        }

        //private void Search_City(object sender, KeyRoutedEventArgs e)
        //{
        //    if (e.Key == VirtualKey.Enter)
        //    {
        //        getWeather(sender.ToString());
        //    }
        //}


        //private void MenuSelected(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        //{
        //    var item = NavigationMenu.SelectedItem as NavigationViewItem;

        //    switch (item.Tag)
        //    {
        //        case "current":
        //            ContentFrame.Navigate(typeof(CurrentWeather));
        //            break;
        //    }
        //}
    }
}
