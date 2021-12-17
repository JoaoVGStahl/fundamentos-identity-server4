using Microsoft.AspNetCore.Http;
using SkyCommerce.Extensions;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SkyCommerce.Site.Service
{
    public class GeoposicaoService : IGeoposicaoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserGeoLocation _userGeoLocation;

        public GeoposicaoService(
            IHttpContextAccessor httpContextAccessor,
            IUserGeoLocation userGeoLocation)
        {
            _httpContextAccessor = httpContextAccessor;
            _userGeoLocation = userGeoLocation;
        }

        public async Task<string> GetRequestIp(bool tryUseXForwardHeader = true)
        {
            if (IsLocalIpAddress(_httpContextAccessor.HttpContext.Request.Host.Host))
                return await _userGeoLocation.LocalhostIp();

            string ip = null;

            if (tryUseXForwardHeader)
                ip = GetHeaderValueAs<string>(_httpContextAccessor, "X-Forwarded-For")?.Split(",").FirstOrDefault();

            // RemoteIpAddress is always null in DNX RC1 Update1 (bug).
            if (ip.IsMissing() && _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress != null)
                ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (ip.IsMissing())
                ip = GetHeaderValueAs<string>(_httpContextAccessor, "REMOTE_ADDR");

            // _httpContextAccessor.HttpContext?.Request?.Host this is the local host.

            if (ip.IsMissing())
                throw new Exception("Unable to determine caller's IP.");

            // Remove port if on IP address
            ip = ip.Substring(0, ip.IndexOf(":"));

            return ip;
        }

        public async Task<LocalizacaoAtual> ObterLocalizacaoAtual()
        {
            return await _userGeoLocation.GetByIp(await GetRequestIp());
        }


        public async Task<GeoCoordinate> GeolocalizarUsuario()
        {
            var posicaoDoGuerreiro = _httpContextAccessor.HttpContext.Request.Cookies["geoposicao"];
            if (posicaoDoGuerreiro == null)
            {
                var localizacaoDoUsuario = await ObterLocalizacaoAtual();
                posicaoDoGuerreiro = localizacaoDoUsuario.Latitude + "|" + localizacaoDoUsuario.Longitude;
                _httpContextAccessor.HttpContext.Response.Cookies.Append("geoposicao", localizacaoDoUsuario.Latitude + "|" + localizacaoDoUsuario.Longitude);
            }

            return posicaoDoGuerreiro.ToGeoCoordinate();
        }

        public static T GetHeaderValueAs<T>(IHttpContextAccessor httpContextAccessor, string headerName)
        {
            if (httpContextAccessor.HttpContext?.Request?.Headers?.TryGetValue(headerName, out var values) ?? false)
            {
                var rawValues = values.ToString();   // writes out as Csv when there are multiple.

                if (!rawValues.IsMissing())
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
            }
            return default(T);
        }
        public static bool IsLocalIpAddress(string host)
        {
            try
            { // get host IP addresses
                var hostIPs = Dns.GetHostAddresses(host);
                // get local IP addresses
                var localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // test if any host IP equals to any local IP or to localhost
                foreach (var hostIp in hostIPs)
                {
                    // is localhost
                    if (IPAddress.IsLoopback(hostIp)) return true;
                    // is local address
                    foreach (var localIp in localIPs)
                    {
                        if (hostIp.Equals(localIp)) return true;
                    }
                }
            }
            catch { }
            return false;
        }
    }
}
