using SkyCommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyCommerce.Interfaces
{
    public interface IEnderecoStore
    {
        Task<IEnumerable<Endereco>> ObterDoUsuario(string usuario);
    }
}
