using SkyCommerce.Models;
using System.Threading.Tasks;

namespace SkyCommerce.Interfaces
{
    public interface ICarrinhoService
    {
        Task AdicionarProduto(string usuario, Produto produto, int quantidade);
        Task Remover(string produto, string user);
        Task AplicarCupom(string cupom, string user);
        Task AtualizarQuantidadeProduto(string usuario, string produto, int quantidade);
        Task SelecionarFrete(string usuario, string modalidade, GeoCoordinate geolocalizarUsuario, string token);
        Task LimparCarrinho(string usuario);
    }
}
