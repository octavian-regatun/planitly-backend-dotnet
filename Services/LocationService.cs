using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Planitly.Backend.Services
{
    public interface ILocationService
    {
        public Task<LocationFromIpResponse?> GetLocationFromIp(string ip);
    }

    public class LocationService : ILocationService
    {
        private HttpClient _httpClient;
        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LocationFromIpResponse?> GetLocationFromIp(string ip)
        {
            var response = await _httpClient.GetAsync($"http://ip-api.com/json/{ip}");
            var data = await response.Content.ReadFromJsonAsync<LocationFromIpResponse>();

            return data;
        }
    }

    public class LocationFromIpResponse
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lon")]
        public double Lon { get; set; }

    }
}