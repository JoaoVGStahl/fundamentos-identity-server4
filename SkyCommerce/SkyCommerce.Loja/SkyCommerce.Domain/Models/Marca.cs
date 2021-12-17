using Bogus;

namespace SkyCommerce.Models
{
    public class Marca
    {
        public string Nome { get; set; }
        public string NomeUnico { get; set; }
        public string Imagem { get; set; }

        public static Faker<Marca> Obter()
        {
            return new Faker<Marca>("pt_BR")
                .RuleFor(m => m.Nome, f => f.Company.CompanyName())
                .RuleFor(m => m.NomeUnico, f => f.Company.CompanyName().ToLower().Replace(" ", "-"))
                .RuleFor(m => m.Imagem, f => $"/images/brand/{f.IndexFaker+1}.png");
        }
    }
}