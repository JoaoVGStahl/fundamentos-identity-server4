using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;

namespace SkyCommerce.ViewObjects
{
    public class PesquisarProdutoVo : IQuerySort, IQueryPaging
    {
        public string Query { get; set; }

        [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false, HasName = "Marca.NomeUnico")]
        public string Marca { get; set; }

        [QueryOperator(Operator = WhereOperator.Contains, CaseSensitive = false, HasName = "Categorias")]
        public string Categoria { get; set; }

        public string Sort { get; set; }
        public int? Limit { get; set; } = 6;
        public int? Offset { get; set; } = 0;

        public int Pagina(int i)
        {
            return (Limit.GetValueOrDefault(6) * i);
        }
    }
}
