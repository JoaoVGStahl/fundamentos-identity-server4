using Microsoft.EntityFrameworkCore;
using SkyCommerce.Data.Configuration;
using SkyCommerce.Data.Entities;

namespace SkyCommerce.Data.Context
{
    public static class ModelBuilder
    {

        /// <summary>
        /// Configures the client context.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void ConfigureSkyCommerceContext(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasIndex(k => k.NomeUnico).IsUnique();
                e.Property(p => p.Descricao).HasMaxLength(1000);
                e.Property(p => p.Nome).HasMaxLength(100);
                e.Property(p => p.Imagem).HasMaxLength(250);
                e.Property(p => p.Imagens).HasMaxLength(1000);
                e.Property(p => p.Categorias).HasMaxLength(250);
                e.Property(p => p.Detalhes);
                e.OwnsOne(c => c.Embalagem);
                e.HasMany<Avaliacao>().WithOne().HasForeignKey(fk => fk.ProdutoId);
            });

            modelBuilder.Entity<Categoria>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasIndex(k => k.NomeUnico).IsUnique();
            });

            modelBuilder.Entity<Marca>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasIndex(k => k.NomeUnico).IsUnique();
                e.HasMany(m => m.Produtos).WithOne(o => o.Marca).HasForeignKey(fk => fk.MarcaId);
            });

            modelBuilder.Entity<Avaliacao>(e =>
            {
                e.HasKey(k => k.Id);
            });
            modelBuilder.Entity<Endereco>(e =>
            {
                e.HasKey(k => k.Id);
            });

            modelBuilder.Entity<Carrinho>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasIndex(k => k.Usuario).IsUnique();
                e.HasMany(m => m.CarrinhoProdutos).WithOne(o => o.Carrinho).HasForeignKey(f => f.CarrinhoId);
                e.OwnsOne(o => o.Frete);

            });
            modelBuilder.Entity<Pedido>(e =>
            {
                var protecaoCartaoCredito = new DataProtectionConverter();
                e.HasKey(k => k.Id);
                e.HasIndex(k => k.IdentificadorUnico).IsUnique();

                e.OwnsOne(o => o.EnderecoEntrega);
                e.OwnsOne(o => o.EnderecoCobranca);
                e.OwnsOne(o => o.Cartao);

                e.OwnsOne(o => o.Cartao, op =>
                {
                    op.Property(p => p.Numero).HasConversion(protecaoCartaoCredito);
                    op.Property(p => p.CodigoVerificador).HasConversion(protecaoCartaoCredito);
                });

                e.OwnsOne(o => o.EnderecoEntrega);
                e.OwnsOne(o => o.Frete);
                e.HasMany(m => m.Produtos).WithOne(o => o.Pedido).HasForeignKey(fk => fk.PedidoId);
            });


            modelBuilder.Entity<ProdutoVendido>(e =>
            {
                var protecaoCartaoCredito = new DataProtectionConverter();
                e.HasKey(k => k.Id);
            });
        }
    }
}