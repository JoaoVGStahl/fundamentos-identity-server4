using System.Collections.Generic;

namespace SkyCommerce.Data.Entities
{
    internal class Produto
    {
        public int Id { get; set; }
        public int MarcaId { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string NomeUnico { get; set; }
        public string Descricao { get; set; }
        public string Detalhes { get; set; }
        public string Imagem { get; set; }
        public string Imagens { get; set; }
        public decimal Valor { get; set; }
        public int Estoque { get; set; }
        public decimal ValorAntigo { get; set; }
        public bool Novo { get; set; }
        public string Categorias { get; set; }
        public string Cores { get; set; }
        public Models.Embalagem Embalagem { get; set; }
        public Marca Marca { get; set; }
        public ICollection<Entities.Avaliacao> Avaliacoes { get; set; }

        public Produto AtualizarMarca(Marca marca)
        {
            this.MarcaId = marca.Id;
            return this;
        }
    }
}
