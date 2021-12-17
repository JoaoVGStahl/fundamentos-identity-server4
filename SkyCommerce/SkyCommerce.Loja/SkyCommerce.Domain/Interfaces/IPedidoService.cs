using System.Threading.Tasks;
using SkyCommerce.Models;

namespace SkyCommerce.Interfaces
{
    public interface IPedidoService
    {
        Task SalvarPedido(Pedido pedido, string usuario);
    }
}