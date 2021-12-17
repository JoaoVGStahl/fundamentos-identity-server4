using Bogus;
using System.Diagnostics;

namespace SkyCommerce.ViewObjects
{
    [DebuggerDisplay("{Modalidade} - {Valor}")]
    public class Frete
    {
        public Frete() { }
        public Frete(string modalidade, string descricao, in decimal valor)
        {
            Modalidade = modalidade;
            Descricao = descricao;
            Valor = valor;
        }

        public string Modalidade { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public void AtualizarValor(Frete frete)
        {
            Valor += frete.Valor;
        }

        public static Faker<Frete> Obter()
        {
            return new Faker<Frete>()
                .RuleFor(f => f.Modalidade, f => f.Lorem.Word())
                .RuleFor(f => f.Descricao, f => f.Lorem.Word())
                .RuleFor(f => f.Valor, f => f.Random.Decimal(9, 100));
        }

        public static Frete FromViewModel(DetalhesFrete detalhesFrete)
        {
            return new Frete(detalhesFrete.Modalidade, detalhesFrete.Descricao, 0);
        }
    }
}
