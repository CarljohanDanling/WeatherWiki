using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherWiki.Models;

namespace WeatherWiki.DataProvider
{
    public class SuggestionDataProvider
    {
        public async Task<SuggestionRoot> GetSuggestion(string userInput)
        {
            string URL = $"http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/suggest?text={userInput}&f=json";
            SuggestionRoot suggestion = new SuggestionRoot();

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(URL))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<SuggestionRoot>(result.Result);

                    suggestion = data;
                }
            }

            return suggestion;
        }

    }
}
