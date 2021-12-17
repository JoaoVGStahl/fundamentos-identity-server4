using Bogus;

namespace SkyCommerce.Models
{
    public class Categoria
    {
        public string Nome { get; set; }
        public string NomeUnico { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public string ImagemCapa { get; set; }

        public static Faker<Categoria> Obter(string categoria)
        {
            return new Faker<Categoria>("pt_BR")
                .RuleFor(c => c.Nome, f => categoria)
                .RuleFor(c => c.NomeUnico, f => categoria.ToLower())
                .RuleFor(c => c.Descricao, f => f.Commerce.ProductDescription())
                .RuleFor(c => c.Imagem, f => f.Image.PicsumUrl(width: 155, height: 207))
                .RuleFor(c => c.ImagemCapa, f => f.Image.PicsumUrl(width: 1920, height: 800));
        }
    }
}
