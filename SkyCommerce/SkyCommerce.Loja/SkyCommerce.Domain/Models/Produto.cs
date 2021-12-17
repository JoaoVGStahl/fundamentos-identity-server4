using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace SkyCommerce.Models
{
    public class Produto
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string NomeUnico { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorAntigo { get; set; }
        public bool Novo { get; set; }
        public bool Promocao => Valor * 1.10M < ValorAntigo;
        public string Detalhes { get; set; }
        public int Estoque { get; set; }
        public HashSet<string> Imagens { get; set; }
        public HashSet<string> Cores { get; set; }
        public HashSet<string> Categorias { get; set; }
        public bool TemEstoque => Estoque > 0;
        public IEnumerable<Avaliacao> Avaliacoes { get; set; }
        public Embalagem Embalagem { get; set; }
        public Marca Marca { get; set; }

        public static Faker<Produto> Obter(IEnumerable<string> imagens)
        {
            return new Faker<Produto>("pt_BR")
                .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.NomeUnico, f => f.Commerce.ProductName().ToLower().Replace(" ", "-"))
                .RuleFor(p => p.Valor, f => f.Finance.Amount(1, 500))
                .RuleFor(p => p.ValorAntigo, (faker, produto) => faker.Finance.Amount(produto.Valor, produto.Valor * 1.60m))
                .RuleFor(p => p.Descricao, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Categorias, f => f.Commerce.Categories(f.Random.Int(1, 3)).ToHashSet())
                .RuleFor(p => p.Cores, f => f.Make(f.Random.Int(1, 4), () => f.Commerce.Color()).ToHashSet())
                .RuleFor(p => p.Novo, f => f.Random.Bool(0.3f))
                .RuleFor(p => p.Codigo, f => f.Commerce.Ean8())
                .RuleFor(p => p.Imagem, f => f.PickRandom(imagens))
                .RuleFor(p => p.Detalhes, f => f.Lorem.Paragraphs(4))
                .RuleFor(p => p.Estoque, f => f.Random.Int(0, 10))
                .RuleFor(p => p.Imagens, f => f.PickRandom(imagens, 3).ToHashSet())
                .RuleFor(p => p.Embalagem, f => Models.Embalagem.Obter().Generate())
                .RuleFor(p => p.Avaliacoes, (faker, produto) => Avaliacao.Obter(produto.NomeUnico).Generate(faker.Random.Int(1, 5)));
        }

        public string TextoPercentualDesconto()
        {
            if (Valor * 1.60m < ValorAntigo)
            {
                return "60% OFF";
            }
            if (Valor * 1.50m < ValorAntigo)
            {
                return "50% OFF";
            }
            if (Valor * 1.40m < ValorAntigo)
            {
                return "40% OFF";
            }
            if (Valor * 1.30m < ValorAntigo)
            {
                return "30% OFF";
            }
            if (Valor * 1.20m < ValorAntigo)
            {
                return "20% OFF";
            }
            if (Valor * 1.15m < ValorAntigo)
            {
                return "15% OFF";
            }
            if (Valor * 1.10m < ValorAntigo)
            {
                return "10% OFF";
            }

            return string.Empty;
        }
    }
}
