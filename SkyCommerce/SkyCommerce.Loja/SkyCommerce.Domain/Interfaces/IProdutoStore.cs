using SkyCommerce.Domain.Interfaces;
using SkyCommerce.Models;
using SkyCommerce.ViewObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyCommerce.Interfaces
{
    public interface IProdutoStore : IStore<Models.Produto>
    {
        Task<List<Models.Produto>> ObterTodos();
        Task<Models.Produto> ObterPorNome(string nomeUnico);
        Task<IEnumerable<Models.Produto>> ObterPorCategoria(string categoria);
        Task<ListOf<Produto>> Pesquisar(PesquisarProdutoVo model);
        Task<ListOf<Produto>> PesquisarPorCategoria(PesquisarProdutoVo model);
        Task<ListOf<Produto>> PesquisarPorMarca(PesquisarProdutoVo model);
    }
}
