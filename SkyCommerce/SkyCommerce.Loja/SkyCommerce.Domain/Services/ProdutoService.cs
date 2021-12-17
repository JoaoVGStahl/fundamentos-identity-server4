using SkyCommerce.Domain.Interfaces;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using System.Threading.Tasks;

namespace SkyCommerce.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoStore _produtoStore;
        private readonly IAvaliacaoStore _avaliacaoStore;

        public ProdutoService(
            IProdutoStore produtoStore,
            IAvaliacaoStore avaliacaoStore)
        {
            _produtoStore = produtoStore;
            _avaliacaoStore = avaliacaoStore;
        }

        public async Task Comentar(Avaliacao avaliacao)
        {
            var produto = await _produtoStore.ObterPorNome(avaliacao.ProdutoUrl);
            if (produto != null)
            {
                await _avaliacaoStore.SalvarAvaliacao(avaliacao);
            }

        }

    }
}
