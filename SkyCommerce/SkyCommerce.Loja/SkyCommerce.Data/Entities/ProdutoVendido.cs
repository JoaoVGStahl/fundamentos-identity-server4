namespace SkyCommerce.Data.Entities
{
    internal class ProdutoVendido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string Nome { get; set; }
        public string NomeUnico { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public Pedido Pedido { get; set; }
    }
}