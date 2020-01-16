using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WeatherWiki.Models
{
    public class Weather
    {
        [JsonProperty("icon")]
        public string WeatherIcon { get; set; }

        [JsonProperty("description")]
        public string ConditionDescription { get; set; }
    }

    public class WeatherRoot
    {
        [JsonProperty("data")]
        public List<WeatherData> WeatherData { get; set; }

        [JsonProperty("city_name")]
        public string City { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("timestamp_utc")]
        public string HourOfDay { get; set; }

        [JsonProperty("high_temp")]
        public double HighTemperature { get; set; }

        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        [JsonProperty("low_temp")]
        public double LowTemperature { get; set; }
        public string datetime { get; set; }

        [JsonProperty("temp")]
        public double CurrentTemperature { get; set; }
    }
}
