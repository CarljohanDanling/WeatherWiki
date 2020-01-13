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
        private ObservableCollection<string> suggestions;

        public MainPage()
        {
            this.InitializeComponent();
            APIHelper.InitializeClient();
            ApplicationView.PreferredLaunchViewSize = new Size(1120, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            suggestions = new ObservableCollection<string>();
        }

        private async void getWeather(string userInput)
        {
            var gdp = new GeneralDataProvider();
            var weather = await gdp.GetData<WeatherRoot>(userInput, "weather");

            if (weather != null)
            {
                CurrentWeatherComponent.AddCurrentWeatherDataToUI(weather);
                ForecastWeatherComponent.AddForecastWeatherDataToUI(weather.WeatherData);
                errorMessage.Text = " ";
                return;
            }

            errorMessage.Text = "Invalid city name";
        }

        private async Task<List<Suggestion>> getSuggestions(string userInput)
        {
            var gdp = new GeneralDataProvider();
            var suggestion = await gdp.GetData<SuggestionRoot>(userInput, "suggestion");

            return suggestion.Suggestions;
        }

        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput && sender.Text.Length > 2)
            {
                var listOfSuggestions = await getSuggestions(sender.Text);

                if (listOfSuggestions != null)
                {
                    suggestions.Clear();
                    listOfSuggestions.ForEach(x => suggestions.Add(x.SuggestedValue));
                    sender.ItemsSource = suggestions;
                }
            }

            if (sender.Text.Length < 1) // Clears all suggestions if user deletes the search string.
            {
                suggestions.Clear();
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

            // If user presses 'enter' instead of clicking on a suggestion.
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
    }
}
