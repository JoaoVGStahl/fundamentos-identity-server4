using System.Collections.Generic;
using System.Threading.Tasks;
using SkyCommerce.Domain.Interfaces;

namespace SkyCommerce.Interfaces
{
    public interface ICategoriaStore : IStore<Models.Categoria>
    {
        Task<List<Models.Categoria>> ObterTodos();
        Task<Models.Categoria> ObterPorNome(string nomeUnico);
    }
}