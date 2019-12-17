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
    public class WeatherDataProvider
    {
        public async Task<WeatherRoot> GetWeather(string cityName)
        {
            string URL = $"https://api.weatherbit.io/v2.0/forecast/daily?city={cityName}&days=7&key=b11292cc43f94dbcb931a25da7d56660";
            WeatherRoot weatherData = new WeatherRoot();

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(URL))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<WeatherRoot>(result.Result);

                    weatherData = data;
                }

                else
                {
                    return null;
                }
            }

            return weatherData;
        }
    }
}
