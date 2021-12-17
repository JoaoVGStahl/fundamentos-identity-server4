using SkyCommerce.Models;
using SkyCommerce.ViewObjects;
using System.Collections.Generic;
using System.Linq;

namespace SkyCommerce.Site.Models
{
    public class ProdutoDetalhesViewModel
    {
        public Produto Produto { get; set; }
        public IEnumerable<Produto> ProdutosRelacionados { get; set; }
        public IEnumerable<DetalhesFrete> OpcoesFrete { get; set; }

        public double NotaMedia()
        {
            return (double)Produto.Avaliacoes.Sum(s => s.Nota) / (double)Produto.Avaliacoes.Count();
        }
    }
}
