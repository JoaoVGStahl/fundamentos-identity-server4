namespace SkyCommerce.Data.Entities
{
    internal class ItemCarrinho
    {
        public ItemCarrinho() { }
        public ItemCarrinho(Models.Produto produto, Carrinho carrinho, int quantidade)
        {
            NomeProduto = produto.Nome;
            NomeUnico = produto.NomeUnico;
            Valor = produto.Valor;
            CarrinhoId = carrinho.Id;
            Quantidade = quantidade;
            Imagem = produto.Imagem;
        }

        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public string NomeUnico { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public int CarrinhoId { get; set; }
        public int Quantidade { get; set; }
        public Carrinho Carrinho { get; set; }

        public void Atualizar(Models.ItemCarrinho item)
        {
            NomeProduto = item.Produto;
            Imagem = item.Imagem;
            Valor = item.Valor;
            Quantidade = item.Quantidade;
        }
    }
}