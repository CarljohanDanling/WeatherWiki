using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherWiki.DataProvider;
using WeatherWiki.Models;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace WeatherWiki
{
    public sealed partial class MainPage : Page
    {
        // Collection with autosuggestions.
        private ObservableCollection<String> suggestions;

        public MainPage()
        {
            this.InitializeComponent();
            APIHelper.InitializeClient();

            // Setting preferred launch size.
            ApplicationView.PreferredLaunchViewSize = new Size(1120, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            suggestions = new ObservableCollection<string>();
        }

        private async void getWeather(string userInput)
        {
            var gdp = new GeneralDataProvider();
            var weather  = await gdp.GetDataGeneric<WeatherRoot>(new ApiTagger
            {
                Input = userInput,
                TypeOfApi = "weather"
            });

            if (weather == null)
            {
                errorMessage.Text = "Invalid city name";
                return;
            }

            errorMessage.Text = " ";

            CurrentWeatherComponent.AddCurrentWeatherDataToUI(weather);
            ForecastWeatherComponent.AddForecastWeatherDataToUI(weather);
        }

        private async Task<List<Suggestion>> getSuggestion(string userInput)
        {
            var gdp = new GeneralDataProvider();
            var suggestion = await gdp.GetDataGeneric<SuggestionRoot>(new ApiTagger
            {
                Input = userInput,
                TypeOfApi = "suggestion"
            });

            return suggestion.Suggestions;
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

                suggestions.Clear();
                listOfSuggestions.ForEach(x => suggestions.Add(x.SuggestedValue));
                sender.ItemsSource = suggestions;
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            // Error handeling.
            if (args.QueryText.Equals(""))
            {
                errorMessage.Text = "Need valid input before searching...";
                return;
            }

            // If user presses 'enter' instead of clicking suggestion.
            else if (args.ChosenSuggestion == null)
            {
                getWeather(StringCleaner(args.QueryText));
            }

            // User chooses an suggestion.
            else
            {
                getWeather(StringCleaner(args.ChosenSuggestion.ToString()));
            }
        }

        private string StringCleaner(string input)
        {
            if (input.Contains(","))
            {
                return input.Substring(0, input.IndexOf(","));
            }

            return input;
        }


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
