using SkyCommerce.Models;
using System.Collections.Generic;

namespace SkyCommerce.Site.Models
{
    public class ListarProdutosViewModel
    {
        public IEnumerable<Produto> NovoProdutos { get; set; }
        public IEnumerable<Produto> Vitrine { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
        public IEnumerable<Produto> ProdutosEmDestaque { get; set; }
        public IEnumerable<Marca> Marcas { get; set; }
    }
}
