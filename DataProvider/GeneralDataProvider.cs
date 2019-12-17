﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherWiki.Models;

namespace WeatherWiki.DataProvider
{
    public class GeneralDataProvider
    {
        public async Task<T> GenericMethodTest<T>(ApiTagger apiTagger) 
        {
            string URL = UrlBuilder(apiTagger);
            var providedData = new object();

            using (HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(URL))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<T>(result.Result);

                    providedData = data;
                }
            }

            return (T)providedData;
        }

        private string UrlBuilder(ApiTagger apiTagger)
        {
            if (apiTagger.TypeOfApi == "weather")
            {
                return $"https://api.weatherbit.io/v2.0/forecast/daily?city={apiTagger.Input}&days=7&key=b11292cc43f94dbcb931a25da7d56660";
            }

            return $"http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/suggest?text={apiTagger.Input}&f=json";
        }
    }
}
