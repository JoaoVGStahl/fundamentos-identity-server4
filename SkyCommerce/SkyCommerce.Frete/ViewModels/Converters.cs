using SkyCommerce.Fretes.Model;

namespace SkyCommerce.Fretes.ViewModels
{
    public static class Converters
    {

        public static CalculoFreteViewModel ToViewModel(this Frete frete, decimal valor)
        {
            return new CalculoFreteViewModel()
            {
                Modalidade = frete.Modalidade,
                Descricao = frete.Descricao,
                Valor = valor
            };
        }
        public static FreteViewModel ToViewModel(this Frete frete)
        {
            return new FreteViewModel()
            {
                Modalidade = frete.Modalidade,
                Descricao = frete.Descricao,
                Multiplicador = frete.Multiplicador,
                Ativo = frete.Ativo,
                ValorMinimo = frete.ValorMinimo
            };
        }
    }
}