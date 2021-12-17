using Bogus;
using System.Collections.Generic;
using System.Linq;
using SkyCommerce.ViewObjects;

namespace SkyCommerce.Models
{
    public class Carrinho
    {
        public string Usuario { get; set; }
        public List<ItemCarrinho> Items { get; set; }
        public Frete Frete { get; set; }
        public string Cupom { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal Desconto { get; set; } = 0;

        public void AplicarCumpom(string cupom)
        {
            var faker = new Faker();
            Cupom = cupom;
            PercentualDesconto = faker.Random.Decimal(0, 40);
            Desconto = Total * PercentualDesconto / 100;
        }

        public bool Possui(Produto produto)
        {
            return Items != null && Items.Any(a => a.NomeUnico.Equals(produto.NomeUnico));
        }

        public decimal TotalProdutos => Items.Sum(s => s.Valor * s.Quantidade);

        public decimal Imposto => TotalProdutos * 0.3m;

        public decimal TotalSemImposto => TotalProdutos - Imposto;

        public decimal Total => TotalProdutos + (Frete?.Valor ?? 0) - Desconto;

        public bool Possui(string produto)
        {
            return Items != null && Items.Any(a => a.NomeUnico.Equals(produto));
        }

        public void SelecionarFrete(Frete frete)
        {
            Frete = frete;
        }

        public ItemCarrinho AtualizarQuantidade(string produto, in int quantidade)
        {
            Frete = null;

            var item = Items.First(f => f.NomeUnico.Equals(produto));
            item.Quantidade = quantidade;
            return item;
        }

        public ItemCarrinho AdicionarProduto(Produto produto, in int quantidade)
        {
            Frete = null;

            var item = new ItemCarrinho(produto, quantidade);
            Items.Add(item);
            return item;
        }

        public IEnumerable<SnapshotProduto> Snapshot()
        {
            return Items.Select(s => new SnapshotProduto(s));
        }

        public bool FreteSelecionado()
        {
            return TotalProdutos >= 200 || Frete != null;
        }
    }
}
