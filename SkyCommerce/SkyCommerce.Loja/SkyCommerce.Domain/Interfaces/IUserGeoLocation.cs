using SkyCommerce.Models;
using System.Threading.Tasks;

namespace SkyCommerce.Interfaces
{
    public interface IUserGeoLocation
    {
        Task<LocalizacaoAtual> GetByIp(string ip);
        Task<string> LocalhostIp();
    }
}
