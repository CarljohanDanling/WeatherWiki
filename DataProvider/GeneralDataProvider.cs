using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherWiki.DataProvider
{
    public class GeneralDataProvider
    {
        public async Task<T> GetData<T>(string userInput, string typeOfApi) where T:class
        {
            string URL = UrlBuilder(userInput, typeOfApi);
            T providedData = default;

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(URL))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<T>(result.Result);

                    providedData = data;
                }
            }

            return providedData;
        }

        private string UrlBuilder(string userInput, string typeOfApi)
        {
            switch (typeOfApi)
            {
                case "weather-daily":
                    return $"https://api.weatherbit.io/v2.0/forecast/daily?city={userInput}&days=7&key=51305649a13d46a3a372ab025beeef4f";
                case "weather-hourly":
                    return $"https://api.weatherbit.io/v2.0/forecast/hourly?city={userInput}&key=51305649a13d46a3a372ab025beeef4f&hours=24";
                default: 
                    return $"http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/suggest?text={userInput}&f=json";
            }
        }
    }
}
