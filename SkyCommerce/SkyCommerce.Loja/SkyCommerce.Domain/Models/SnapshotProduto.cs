using Bogus;
using System.Collections.Generic;

namespace SkyCommerce.Models
{
    public class SnapshotProduto
    {
        public SnapshotProduto() { }
        public SnapshotProduto(ItemCarrinho produto)
        {
            NomeUnico = produto.NomeUnico;
            Nome = produto.Produto;
            Valor = produto.Valor;
            Imagem = produto.Imagem;
            Quantidade = produto.Quantidade;
        }
        public string Nome { get; set; }
        public string NomeUnico { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public decimal Total => Valor * Quantidade;

        public static Faker<SnapshotProduto> Obter(IEnumerable<string> imagens)
        {
            return new Faker<SnapshotProduto>("pt_BR")
                .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.NomeUnico, f => f.Commerce.ProductName().ToLower().Replace(" ", "-"))
                .RuleFor(p => p.Valor, f => f.Finance.Amount(1, 500))
                .RuleFor(p => p.Quantidade, f => f.Random.Int(1, 2))
                .RuleFor(p => p.Imagem, f => f.PickRandom(imagens));
        }
    }
}