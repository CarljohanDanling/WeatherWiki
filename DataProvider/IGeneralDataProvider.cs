using WeatherWiki.Models;

namespace WeatherWiki.DataProvider
{
    public interface IGeneralDataProvider
    {
        WeatherRoot WeatherRoot { get; set; }
        SuggestionRoot Suggestion { get; set; }
    }
}
