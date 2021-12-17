using System.Collections.Generic;

namespace SkyCommerce.Data.Entities
{
    internal class Marca
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeUnico { get; set; }
        public string Imagem { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}
