namespace SkyCommerce.Models
{
    public class ItemCarrinho
    {
        public ItemCarrinho() { }
        public ItemCarrinho(Produto produto, int quantidade)
        {
            NomeUnico = produto.NomeUnico;
            Produto = produto.Nome;
            Imagem = produto.Imagem;
            Quantidade = quantidade;
            Valor = produto.Valor;
        }

        public string NomeUnico { get; set; }
        public string Produto { get; set; }
        public string Imagem { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }

        public decimal Total => Valor * Quantidade;
    }
}