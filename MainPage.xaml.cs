using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WeatherWiki.DataProvider;
using WeatherWiki.Models;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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
            ApplicationView.PreferredLaunchViewSize = new Size(1120, 740);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            suggestions = new ObservableCollection<string>();
        }

        private async void GetWeatherDaily(string userInput)
        {
            var gdp = new GeneralDataProvider();
            var weatherDaily = await gdp.GetData<WeatherRoot>(userInput, "weather-daily");

            if (weatherDaily != null)
            {
                CurrentWeatherComponent.AddCurrentWeatherDataToUI(weatherDaily);
                DailyForecastWeatherComponent.AddDailyForecastWeatherDataToUI(weatherDaily.WeatherData);

                errorMessage.Text = " ";
                return;
            }

            errorMessage.Text = "Invalid city name";
        }

        private async void GetWeatherHourly(string userInput, string typeOfData)
        {
            var gdp = new GeneralDataProvider();
            var weatherHourly = await gdp.GetData<WeatherRoot>(userInput, "weather-hourly");

            if (weatherHourly != null)
            {
                HourlyForecastWeatherComponent.AddHourlyForecastWeatherDataToUI(weatherHourly, typeOfData);
                HourByHourTextBlock.Visibility = 0;
            }

            return;
        }

        private async Task<List<Suggestion>> GetSuggestions(string userInput)
        {
            var gdp = new GeneralDataProvider();
            var suggestion = await gdp.GetData<SuggestionRoot>(userInput, "suggestion");

            return suggestion.Suggestions;
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            bool isUserInputReason = args.Reason == 0; // 0 is for UserInput.
            bool isUserInputMinimumLength = sender.Text.Length > 2;

            if (isUserInputReason && isUserInputMinimumLength)
            {
                PopulateAutoSuggestionBox(sender);
                return;
            }

            if (string.IsNullOrEmpty(sender.Text))
            {
                suggestions.Clear();
            }
        }

        private async void PopulateAutoSuggestionBox(AutoSuggestBox sender)
        {
            var listOfSuggestions = await GetSuggestions(sender.Text);

            if (listOfSuggestions != null)
            {
                suggestions.Clear();
                listOfSuggestions.ForEach(x => suggestions.Add(x.SuggestedValue));
                sender.ItemsSource = suggestions;
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            // Error handeling.
            if (string.IsNullOrEmpty(args.QueryText))
            {
                errorMessage.Text = "Need valid input before searching...";
                return;
            }

            // If user presses 'enter' instead of clicking on a suggestion.
            else if (args.ChosenSuggestion == null)
            {
                GetWeatherDaily(StringCleaner(args.QueryText));
                GetWeatherHourly(StringCleaner(args.QueryText), "normal");
            }

            // User chooses an suggestion.
            else
            {
                GetWeatherDaily(StringCleaner(args.ChosenSuggestion.ToString()));
                GetWeatherHourly(StringCleaner(args.ChosenSuggestion.ToString()), "normal");
            }
        }

        private string StringCleaner(string input)
        {
            return input.Contains(",") ? input.Substring(0, input.IndexOf(",")) : input;
        }

        private void OnTapDailyForecastChangeIndividualHourlyForecast(object sender, TappedRoutedEventArgs e)
        {
            GetWeatherHourly(StringCleaner(txtAutoSuggestBox.Text), "randomized");
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DailyForecastWeatherComponent.individualDayTapped += new TappedEventHandler(OnTapDailyForecastChangeIndividualHourlyForecast);
        }
    }
}
