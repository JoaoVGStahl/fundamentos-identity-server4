using SkyCommerce.Models;
using System.Threading.Tasks;

namespace SkyCommerce.Interfaces
{
    public interface IAvaliacaoStore
    {
        Task SalvarAvaliacao(Avaliacao avaliacao);
    }
}
