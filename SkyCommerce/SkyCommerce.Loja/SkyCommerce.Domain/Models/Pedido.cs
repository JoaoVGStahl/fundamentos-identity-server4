using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using SkyCommerce.ViewObjects;

namespace SkyCommerce.Models
{
    public class Pedido
    {
        public Pedido() { }
        public Pedido(Frete frete, Endereco enderecoCobranca, Endereco enderecoEntrega, CartaoCredito cartaoCredito,
            TipoPagamento tipoPagamento, Carrinho carrinho, string comentario)
        {
            var faker = new Faker();
            Frete = frete;
            Produtos = carrinho.Snapshot();
            Cartao = cartaoCredito;
            EnderecoEntrega = enderecoEntrega;
            EnderecoCobranca = enderecoCobranca;
            TipoPagamento = tipoPagamento;
            Comentario = comentario;
            Cupom = carrinho.Cupom;
            Desconto = carrinho.Desconto;
            StatusPedido = StatusPedido.AguardandoConfirmacao;
            DataPagamento = DateTime.Now;
            IdentificadorUnico = faker.Random.AlphaNumeric(9).ToUpper();
            RastreamentoFrete = faker.Random.AlphaNumeric(9).ToUpper();
        }

        public Endereco EnderecoEntrega { get; set; }
        public Endereco EnderecoCobranca { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public string Comentario { get; }
        public CartaoCredito Cartao { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public DateTime DataPagamento { get; set; }
        public string IdentificadorUnico { get; set; }
        public IEnumerable<SnapshotProduto> Produtos { get; set; }
        public string Cupom { get; set; }
        public decimal Desconto { get; set; }
        public decimal PercentualDesconto => Desconto / Total;
        public string RastreamentoFrete { get; set; }
        public Frete Frete { get; set; }

        public decimal TotalProdutos => Produtos.Sum(s => s.Valor * s.Quantidade);

        public decimal Imposto => TotalProdutos * 0.3m;

        public decimal TotalSemImposto => TotalProdutos - Imposto;

        public decimal Total => TotalProdutos + Frete.Valor - Desconto;
        public int QuantidadeProdutos => Produtos.Count();

        public static Faker<Pedido> Obter(string nome, IEnumerable<string> imagens)
        {
            var faker = new Faker("pt_BR");
            return new Faker<Pedido>("pt_BR")
                .RuleFor(p => p.EnderecoEntrega, f => Endereco.Obter(nome))
                .RuleFor(p => p.EnderecoCobranca, f => Endereco.Obter(nome))
                .RuleFor(p => p.TipoPagamento, f => f.PickRandom<TipoPagamento>())
                .RuleFor(p => p.Cartao, f => CartaoCredito.Obter().Generate())
                .RuleFor(p => p.StatusPedido, f => f.PickRandom<StatusPedido>())
                .RuleFor(p => p.DataPagamento, f => f.Date.Past())
                .RuleFor(p => p.IdentificadorUnico, f => f.Random.AlphaNumeric(10).ToUpper())
                .RuleFor(p => p.RastreamentoFrete, f => f.Random.AlphaNumeric(10).ToUpper())
                .RuleFor(p => p.Produtos, f => SnapshotProduto.Obter(imagens).Generate(faker.Random.Int(1, 10)))
                .RuleFor(p => p.Cupom, f => f.Lorem.Word())
                .RuleFor(p => p.Frete, f => Frete.Obter().Generate())
                .RuleFor(p => p.Desconto, (f, pedido) => f.Finance.Amount(0, pedido.Total * 0.60m));
        }

    }
}