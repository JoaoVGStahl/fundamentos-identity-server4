using SkyCommerce.Models;
using System.Collections.Generic;
using System.Linq;
using SkyCommerce.ViewObjects;

namespace SkyCommerce.Extensions
{
    public static class FreteExtensions
    {
        public static Frete Modalidade(this IEnumerable<Frete> fretes, string modalidade) =>
            fretes.FirstOrDefault(f => f.Modalidade.ToUpper().Equals(modalidade.ToUpper()));
    }
}
