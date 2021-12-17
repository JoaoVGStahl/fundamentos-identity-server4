using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using System.Threading.Tasks;

namespace SkyCommerce.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoStore _pedidoStore;

        public PedidoService(IPedidoStore pedidoStore)
        {
            _pedidoStore = pedidoStore;
        }

        public Task SalvarPedido(Pedido pedido, string usuario)
        {
            return _pedidoStore.SalvarPedido(pedido, usuario);
        }
    }
}
