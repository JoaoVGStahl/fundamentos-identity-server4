using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SkyCommerce.Extensions;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkyCommerce.CrossCutting.Services
{
    public class GeoLocateUsers : IUserGeoLocation
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public GeoLocateUsers(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        public async Task<LocalizacaoAtual> GetByIp(string ip)
        {
            if (ip.IsMissing())
                return null;

            var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.ipstack.com/{ip}?access_key={_configuration["IpStack"]}&format=1");

            var client = _clientFactory.CreateClient("IpStack");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var userLocal = JsonConvert.DeserializeObject<UserLocalizationData>(responseStream);
                return new LocalizacaoAtual()
                {
                    Cep = userLocal.Zip,
                    Cidade = userLocal.City,
                    Continente = userLocal.ContinentName,
                    HostName = userLocal.HostName,
                    Ip = userLocal.Ip,
                    Latitude = userLocal.Latitude,
                    Longitude = userLocal.Longitude,
                    Pais = userLocal.CountryName,
                    Regiao = userLocal.RegionName,
                };
            }

            return null;
        }

        public async Task<string> LocalhostIp()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.myip.com/");

            var client = _clientFactory.CreateClient("myip");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var userLocal = JsonConvert.DeserializeObject<MyIpApi>(responseStream);
                return userLocal.ip;
            }

            return null;

        }

    }
}
