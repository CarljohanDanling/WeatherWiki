using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherWiki.Models
{
    public class Suggestion
    {
        [JsonProperty("text")]
        public string SuggestedValue { get; set; }
    }

    public class SuggestionRoot
    {
        [JsonProperty("suggestions")]
        public List<Suggestion> Suggestions { get; set; }
    }
}
