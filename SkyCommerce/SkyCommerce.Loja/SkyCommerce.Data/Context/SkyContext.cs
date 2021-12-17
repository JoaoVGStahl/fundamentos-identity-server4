using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkyCommerce.Data.Entities;
using System.Threading.Tasks;

namespace SkyCommerce.Data.Context
{
    public class SkyContext : DbContext, IDataProtectionKeyContext
    {

        public SkyContext(DbContextOptions<SkyContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureSkyCommerceContext();
        }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        internal DbSet<Produto> Produtos { get; set; }
        internal DbSet<Categoria> Categorias { get; set; }
        internal DbSet<Avaliacao> Avaliacoes { get; set; }
        internal DbSet<Marca> Marcas { get; set; }
        internal DbSet<Carrinho> Carrinho { get; set; }
        internal DbSet<ItemCarrinho> CarrinhoProdutos { get; set; }
        internal DbSet<Endereco> Enderecos { get; set; }
        internal DbSet<Pedido> Pedidos { get; set; }
        internal DbSet<ProdutoVendido> ProdutosVendido { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(true);
        }
    }
}