using System.Collections.Generic;
using System.Threading.Tasks;
using SkyCommerce.Models;

namespace SkyCommerce.Interfaces
{
    public interface IMarcaStore
    {
        Task<IEnumerable<Marca>> ObterTodos();
        Task<Marca> ObterPorNome(string marca);
    }
}
