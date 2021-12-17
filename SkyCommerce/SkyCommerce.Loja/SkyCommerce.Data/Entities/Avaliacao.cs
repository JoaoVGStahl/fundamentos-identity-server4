using System;

namespace SkyCommerce.Data.Entities
{
    internal class Avaliacao
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string Usuario { get; set; }
        public DateTime DataAvaliacao { get; set; }
        public string Titulo { get; set; }
        public string Comentario { get; set; }
        public double Nota { get; set; }
        public string Imagem { get; set; }
        public Entities.Produto Produto { get; set; }
    }
}