namespace SkyCommerce.ViewObjects
{
    public class DetalhesFrete
    {
        public bool Ativo { get; set; }
        public string Modalidade { get; set; }
        public string Descricao { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal Multiplicador { get; set; }
    }
}