using SkyCommerce.Models;
using System.Threading.Tasks;

namespace SkyCommerce.Interfaces
{
    public interface IProdutoService
    {
        Task Comentar(Avaliacao avaliacao);
    }
}