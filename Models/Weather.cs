using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherWiki.Models
{
    public class Weather
    {
        [JsonProperty("icon")]
        public string WeatherIcon { get; set; }

        [JsonProperty("code")]
        public int ConditionCode { get; set; }

        [JsonProperty("description")]
        public string ConditionDescription { get; set; }
    }

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


    public class WeatherData
    {
        public double? moonrise_ts { get; set; }
        public string wind_cdir { get; set; }
        public int rh { get; set; }
        public double pres { get; set; }

        [JsonProperty("high_temp")]
        public double HighTemperature { get; set; }

        public int? sunset_ts { get; set; }
        public double ozone { get; set; }
        public double moon_phase { get; set; }
        public double wind_gust_spd { get; set; }
        public double snow_depth { get; set; }  
        public int clouds { get; set; }
        public int ts { get; set; }
        public int? sunrise_ts { get; set; }
        public double app_min_temp { get; set; }
        public double wind_spd { get; set; }
        public int pop { get; set; }
        public string wind_cdir_full { get; set; }
        public double slp { get; set; }

        [JsonProperty("valid_date")]
        public string Date { get; set; }

        public double app_max_temp { get; set; }
        public double vis { get; set; }
        public double dewpt { get; set; }
        public double snow { get; set; }
        public double uv { get; set; }

        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        public int wind_dir { get; set; }
        public object max_dhi { get; set; }
        public int clouds_hi { get; set; }
        public double precip { get; set; }

        [JsonProperty("low_temp")]
        public double LowTemperature { get; set; }
        public double max_temp { get; set; }
        public int? moonset_ts { get; set; }
        public string datetime { get; set; }

        [JsonProperty("temp")]
        public double CurrentTemperature { get; set; }

        public double min_temp { get; set; }
        public int clouds_mid { get; set; }
        public int clouds_low { get; set; }
    }
}
