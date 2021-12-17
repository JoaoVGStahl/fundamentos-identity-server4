using System;
using System.Threading.Tasks;

namespace SkyCommerce.Domain.Interfaces
{
    public interface IStore<TEntity> : IDisposable
    {
        Task Adicionar(TEntity obj);
        Task Atualizar(TEntity obj);
        Task Remover(TEntity obj);
    }
}