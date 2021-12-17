using Microsoft.EntityFrameworkCore;
using SkyCommerce.Data.Context;
using SkyCommerce.Data.Mappers;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using System.Threading.Tasks;

namespace SkyCommerce.Data.Store
{
    public class AvaliacaoStore : IAvaliacaoStore
    {
        internal SkyContext Db { get; set; }
        internal DbSet<Entities.Avaliacao> DbSet { get; set; }
        internal DbSet<Entities.Produto> ProdutosDbSet { get; set; }
        public AvaliacaoStore(SkyContext context)
        {
            this.Db = context;
            this.DbSet = context.Avaliacoes;
            this.ProdutosDbSet = context.Produtos;
        }


        public async Task SalvarAvaliacao(Avaliacao avaliacao)
        {
            var entity = avaliacao.ToEntity();
            var produto = await ProdutosDbSet.Include(i => i.Avaliacoes).FirstOrDefaultAsync(f => f.NomeUnico.ToLower().Equals(avaliacao.ProdutoUrl));
            produto.Avaliacoes.Add(entity);

            await Db.SaveChangesAsync();
        }
    }
}
