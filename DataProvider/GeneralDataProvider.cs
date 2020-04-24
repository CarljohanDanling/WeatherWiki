namespace WeatherWiki.DataProvider
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GeneralDataProvider
    {
        private string ApiKey = Environment.GetEnvironmentVariable("API_KEY_WEATHER");

        public async Task<T> GetData<T>(string userInput, string typeOfApi) where T : class
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
                    return $"https://api.weatherbit.io/v2.0/forecast/daily?city={userInput}&days=7&key={ApiKey}";
                case "weather-hourly":
                    return $"https://api.weatherbit.io/v2.0/forecast/hourly?city={userInput}&key={ApiKey}&hours=24";
                default:
                    return $"http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/suggest?text={userInput}&f=json";
            }
        }
    }
}
