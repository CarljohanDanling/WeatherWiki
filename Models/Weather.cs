using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWiki.Models
{
    public class WeatherRoot
    {
        [JsonProperty("data")]
        public List<WeatherData> WeatherData { get; set; }

        [JsonProperty("city_name")]
        public string City { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("state_code")]
        public string StateCode { get; set; }
    }

    public class Weather
    {
        [JsonProperty("icon")]
        public string WeatherIcon { get; set; }

        [JsonProperty("code")]
        public int ConditionCode { get; set; }

        [JsonProperty("description")]
        public string ConditionDescription { get; set; }
    }
}
