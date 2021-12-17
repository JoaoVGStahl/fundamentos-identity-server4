using Microsoft.EntityFrameworkCore;
using SkyCommerce.Data.Context;
using SkyCommerce.Data.Mappers;
using SkyCommerce.Interfaces;
using SkyCommerce.Models;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SkyCommerce.Data.Store
{
    public class CarrinhoStore : ICarrinhoStore
    {
        internal SkyContext Db { get; set; }
        public CarrinhoStore(SkyContext context)
        {
            this.Db = context;
        }

        public async Task<Models.Carrinho> ObterCarrinho(string usuario)
        {
            var itensCarrinho = await Db.CarrinhoProdutos.AsNoTracking()
                .Include(i => i.Carrinho)
                .Where(w => w.Carrinho.Usuario == usuario)
                .ToListAsync();

            if (itensCarrinho.Any())
            {
                var carrinho = itensCarrinho.First().Carrinho;

                carrinho.CarrinhoProdutos = itensCarrinho;

                return carrinho.ToModel();
            }

            return null;
        }

        public async Task<Carrinho> CriarCarrinho(string usuario)
        {
            var carrinho = new Entities.Carrinho() { Usuario = usuario };
            await Db.Carrinho.AddAsync(carrinho);
            await Db.SaveChangesAsync();

            return carrinho.ToModel();
        }

        public async Task AdicionarItemAoCarrinho(Carrinho carrinho, Models.ItemCarrinho item)
        {
            var carrinhoDb = await Db.Carrinho
                .Include(i => i.CarrinhoProdutos)
                .FirstOrDefaultAsync(f => f.Usuario == carrinho.Usuario);


            var itemCarrinho = item.ToEntity();
            itemCarrinho.CarrinhoId = carrinhoDb.Id;
            carrinhoDb.Atualizar(carrinho);

            await Db.CarrinhoProdutos.AddAsync(itemCarrinho);

            await Db.SaveChangesAsync();
        }


        public async Task AtualizarItemCarrinho(Models.ItemCarrinho item, Carrinho carrinho)
        {
            var carrinhoDb = await Db.Carrinho
                .Include(i => i.CarrinhoProdutos)
                .FirstOrDefaultAsync(f => f.Usuario == carrinho.Usuario);

            var produto = carrinhoDb.CarrinhoProdutos.FirstOrDefault(w => w.NomeUnico == item.NomeUnico);
            produto?.Atualizar(item);

            carrinhoDb.Atualizar(carrinho);
            await Db.SaveChangesAsync();
        }

        public async Task Remover(string produto, string user)
        {
            var itemCarrinho = await Db.CarrinhoProdutos.Include(i => i.Carrinho)
                .Where(w => w.Carrinho.Usuario == user && w.NomeUnico == produto).ToListAsync();

            Db.CarrinhoProdutos.RemoveRange(itemCarrinho);

            await Db.SaveChangesAsync();
        }

        public async Task AtualizarCarrinho(Carrinho carrinho)
        {
            var carrinhoDb = await Db.Carrinho
                .FirstOrDefaultAsync(f => f.Usuario == carrinho.Usuario);

            carrinhoDb.Atualizar(carrinho);
            await Db.SaveChangesAsync();
        }

        public async Task RemoverTodosItens(string usuario)
        {
            var carrinhoDb = await Db.Carrinho
                .Include(i => i.CarrinhoProdutos)
                .FirstOrDefaultAsync(f => f.Usuario == usuario);

            Db.CarrinhoProdutos.RemoveRange(carrinhoDb.CarrinhoProdutos);
            await Db.SaveChangesAsync();
        }
    }
}
