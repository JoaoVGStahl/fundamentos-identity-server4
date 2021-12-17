using SkyCommerce.Models;
using System.Collections.Generic;
using SkyCommerce.ViewObjects;

namespace SkyCommerce.Site.Models
{
    public class CarrinhoViewModel
    {
        public Carrinho Carrinho { get; set; }
        public IEnumerable<Frete> Fretes { get; set; }
    }
}
