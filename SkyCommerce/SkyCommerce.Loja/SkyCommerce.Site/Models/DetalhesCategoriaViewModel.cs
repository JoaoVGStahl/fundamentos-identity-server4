using SkyCommerce.Models;
using System.Collections.Generic;
using SkyCommerce.ViewObjects;

namespace SkyCommerce.Site.Models
{
    public class ProdutosPrincipalViewModel
    {
        public ListOf<Produto> Produtos { get; set; }
        public string ImagemCapa { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public List<Categoria> Categorias { get; set; }
        public IEnumerable<Marca> Marcas { get; set; }
        public PesquisarProdutoVo PesquisaAtual { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
