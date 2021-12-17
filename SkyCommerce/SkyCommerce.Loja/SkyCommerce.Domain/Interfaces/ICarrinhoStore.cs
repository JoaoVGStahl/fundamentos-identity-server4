using SkyCommerce.Models;
using System.Threading.Tasks;

namespace SkyCommerce.Interfaces
{
    public interface ICarrinhoStore
    {
        Task<Models.Carrinho> ObterCarrinho(string usuario);
        Task<Carrinho> CriarCarrinho(string usuario);
        Task AdicionarItemAoCarrinho(Carrinho carrinho, Models.ItemCarrinho item);
        Task AtualizarItemCarrinho(Models.ItemCarrinho item, Carrinho carrinho);
        Task Remover(string produto, string user);
        Task AtualizarCarrinho(Carrinho carrinho);
        Task RemoverTodosItens(string usuario);
    }
}