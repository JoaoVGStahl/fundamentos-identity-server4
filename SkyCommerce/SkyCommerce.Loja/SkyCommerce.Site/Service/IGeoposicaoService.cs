using SkyCommerce.Models;
using System.Threading.Tasks;

namespace SkyCommerce.Site.Service
{
    public interface IGeoposicaoService
    {
        Task<string> GetRequestIp(bool tryUseXForwardHeader = true);
        Task<LocalizacaoAtual> ObterLocalizacaoAtual();
        Task<GeoCoordinate> GeolocalizarUsuario();
    }
}