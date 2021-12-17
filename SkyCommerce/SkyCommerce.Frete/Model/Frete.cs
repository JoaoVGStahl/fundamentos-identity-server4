using SkyCommerce.Fretes.ViewModels;
using System.Diagnostics;

namespace SkyCommerce.Fretes.Model
{
    [DebuggerDisplay("{Modalidade} - {Valor}")]
    public class Frete
    {
        public Frete() { }
        public Frete(string modalidade, string descricao, in decimal valorMinimo, in decimal multiplicador)
        {
            Modalidade = modalidade;
            Descricao = descricao;
            ValorMinimo = valorMinimo;
            Multiplicador = multiplicador;
            Ativo = true;
        }

        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Modalidade { get; set; }
        public string Descricao { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal Multiplicador { get; set; }

        public decimal CalcularFrete(EmbalagemViewModel embalagem, GeoCoordinate posicao)
        {
            var pesoCubico = (embalagem.Altura * embalagem.Comprimento * embalagem.Largura) / 6000;
            pesoCubico = pesoCubico > embalagem.Peso ? pesoCubico : embalagem.Peso;
            var distancia = posicao.GetDistanceTo(DadosGerais.CentroDistribuicao, DistanceType.Km);
            var valor = (decimal)(distancia * pesoCubico) / 100;
            valor = valor < ValorMinimo ? ValorMinimo : valor;
            return valor * Multiplicador;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}
